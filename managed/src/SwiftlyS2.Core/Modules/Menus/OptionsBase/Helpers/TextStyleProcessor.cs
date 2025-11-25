using System.Text;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus.OptionsBase.Helpers;

internal sealed partial class TextStyleProcessor : IDisposable
{
    private readonly ConcurrentDictionary<string, int> scrollOffsets = new();
    private readonly ConcurrentDictionary<string, string> staticStyleCache = new();

    private volatile bool disposed;

    public TextStyleProcessor()
    {
        disposed = false;
        scrollOffsets.Clear();
        staticStyleCache.Clear();
    }

    ~TextStyleProcessor()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        // Console.WriteLine($"{GetType().Name} has been disposed.");
        disposed = true;

        scrollOffsets.Clear();
        staticStyleCache.Clear();

        GC.SuppressFinalize(this);
    }

    [GeneratedRegex("<.*?>")]
    private static partial Regex HtmlTagRegex();

    public (string styledText, int scrollOffset) ApplyHorizontalStyle( string text, MenuOptionTextStyle textStyle, float maxWidth )
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return (text, -1);
        }

        if (Helper.EstimateTextWidth(StripHtmlTags(text)) <= maxWidth)
        {
            return (text, -1);
        }

        if (textStyle == MenuOptionTextStyle.TruncateEnd || textStyle == MenuOptionTextStyle.TruncateBothEnds)
        {
            // Cache static styles (TruncateEnd and TruncateBothEnds)
            var cacheKey = $"{text}_{textStyle}_{maxWidth}";
            if (staticStyleCache.TryGetValue(cacheKey, out var cachedStyledText))
            {
                return (cachedStyledText, -1);
            }

            var (styledText, scrollOffset) = textStyle switch {
                MenuOptionTextStyle.TruncateEnd => TruncateTextEnd(text, maxWidth),
                MenuOptionTextStyle.TruncateBothEnds => TruncateTextBothEnds(text, maxWidth),
                _ => (text, -1)
            };

            _ = staticStyleCache.TryAdd(cacheKey, styledText);
            return (styledText, scrollOffset);
        }
        else
        {
            // Dynamic styles (scrolling animations)
            return textStyle switch {
                MenuOptionTextStyle.ScrollLeftFade => ScrollTextWithFade(text, maxWidth, true),
                MenuOptionTextStyle.ScrollRightFade => ScrollTextWithFade(text, maxWidth, false),
                MenuOptionTextStyle.ScrollLeftLoop => ScrollTextWithLoop($"{text.TrimEnd()} ", maxWidth, true),
                MenuOptionTextStyle.ScrollRightLoop => ScrollTextWithLoop($" {text.TrimStart()}", maxWidth, false),
                _ => (text, -1)
            };
        }
    }

    private (string styledText, int scrollOffset) ScrollTextWithFade( string text, float maxWidth, bool scrollLeft )
    {
        // Prepare scroll data and validate
        var (plainChars, segments, targetCharCount) = PrepareScrollData(text, maxWidth);
        if (plainChars == null)
        {
            return (text, -1);
        }
        if (targetCharCount == 0)
        {
            return (string.Empty, -1);
        }

        // Update scroll offset (allow scrolling beyond end for complete fade-out)
        var offset = UpdateScrollOffset(StripHtmlTags(text), scrollLeft, plainChars.Length + 1);

        // Calculate visible character range
        var (skipStart, skipEnd) = scrollLeft
            ? (offset, Math.Max(0, plainChars.Length - offset - targetCharCount))
            : (Math.Max(0, plainChars.Length - targetCharCount - offset), offset);

        // Build output with proper HTML tag tracking
        StringBuilder result = new();
        List<string> outputTags = [], activeTags = [];
        var (charIdx, started) = (0, false);

        foreach (var (content, isTag) in segments)
        {
            if (isTag)
            {
                // Track active opening and closing tags
                UpdateTagState(content, activeTags);

                // Output tags within visible window
                if (started)
                {
                    result = result.Append(content);
                    ProcessOpenTag(content, outputTags);
                }
            }
            else
            {
                // Process characters within scroll window
                foreach (var ch in content)
                {
                    if (charIdx >= skipStart && charIdx < plainChars.Length - skipEnd)
                    {
                        // Apply active tags at start of output
                        if (!started)
                        {
                            started = true;
                            activeTags.ForEach(tag => { result = result.Append(tag); ProcessOpenTag(tag, outputTags); });
                        }
                        result = result.Append(ch);
                    }
                    charIdx++;
                }
            }
        }

        CloseOpenTags(result, outputTags);
        // Console.WriteLine($"styledText: {result}, offset: {offset}");
        return (result.ToString(), offset);
    }

    private (string styledText, int scrollOffset) ScrollTextWithLoop( string text, float maxWidth, bool scrollLeft )
    {
        // Prepare scroll data and validate
        var (plainChars, segments, targetCharCount) = PrepareScrollData(text, maxWidth);
        if (plainChars == null)
        {
            return (text, -1);
        }
        if (targetCharCount == 0)
        {
            return (string.Empty, -1);
        }

        // Update scroll offset for circular wrapping
        var offset = UpdateScrollOffset(StripHtmlTags(text), scrollLeft, plainChars.Length);

        // Build character-to-tags mapping for circular access
        Dictionary<int, List<string>> charToActiveTags = [];
        List<string> currentActiveTags = [];
        var currentCharIdx = 0;

        foreach (var (content, isTag) in segments)
        {
            if (isTag)
            {
                // Track active opening and closing tags
                UpdateTagState(content, currentActiveTags);
            }
            else
            {
                // Map each character to its active tags
                foreach (var ch in content)
                {
                    charToActiveTags[currentCharIdx] = [.. currentActiveTags];
                    currentCharIdx++;
                }
            }
        }

        // Build output in circular order with dynamic tag management
        StringBuilder result = new();
        List<string> outputTags = [];
        List<string>? previousTags = null;

        for (var i = 0; i < targetCharCount; i++)
        {
            // Calculate circular character index
            var charIndex = scrollLeft
                ? (offset + i) % plainChars.Length
                : (plainChars.Length - offset + i) % plainChars.Length;
            var currentTags = charToActiveTags.GetValueOrDefault(charIndex, []);

            // Close tags that are no longer active
            if (previousTags != null)
            {
                for (var j = previousTags.Count - 1; j >= 0; j--)
                {
                    if (!currentTags.Contains(previousTags[j]))
                    {
                        var prevTagName = previousTags[j][1..^1].Split(' ')[0];
                        result = result.Append($"</{prevTagName}>");
                        var idx = outputTags.FindLastIndex(t => t.Equals(prevTagName, StringComparison.OrdinalIgnoreCase));
                        if (idx >= 0)
                        {
                            outputTags.RemoveAt(idx);
                        }
                    }
                }
            }

            // Open new tags that are now active
            foreach (var tag in currentTags)
            {
                if (previousTags == null || !previousTags.Contains(tag))
                {
                    result = result.Append(tag);
                    var tagName = tag[1..^1].Split(' ')[0];
                    outputTags.Add(tagName);
                }
            }

            result = result.Append(plainChars[charIndex]);
            previousTags = currentTags;
        }

        CloseOpenTags(result, outputTags);
        return (result.ToString(), offset);
    }

    private static (string styledText, int scrollOffset) TruncateTextEnd( string text, float maxWidth, string suffix = "..." )
    {
        // Reserve space for suffix
        var targetWidth = maxWidth - Helper.EstimateTextWidth(suffix);
        if (targetWidth <= 0)
        {
            return (suffix, -1);
        }

        var segments = ParseHtmlSegments(text);
        StringBuilder result = new();
        List<string> openTags = [];
        var (currentWidth, reachedLimit) = (0f, false);

        foreach (var (content, isTag) in segments)
        {
            switch (isTag, reachedLimit)
            {
                // Preserve HTML tags before reaching limit
                case (true, false):
                    result = result.Append(content);
                    ProcessOpenTag(content, openTags);
                    break;

                // Process plain text characters until width limit
                case (false, false):
                    foreach (var ch in content)
                    {
                        var charWidth = Helper.GetCharWidth(ch);
                        if (currentWidth + charWidth > targetWidth)
                        {
                            reachedLimit = true;
                            break;
                        }
                        result = result.Append(ch);
                        currentWidth += charWidth;
                    }
                    break;
                default:
                    break;
            }
        }

        if (reachedLimit)
        {
            result = result.Append(suffix);
        }

        CloseOpenTags(result, openTags);
        return (result.ToString(), -1);
    }

    private static (string styledText, int scrollOffset) TruncateTextBothEnds( string text, float maxWidth )
    {
        if (string.IsNullOrEmpty(text))
        {
            return (text, -1);
        }

        // Check if text fits without truncation
        var plainText = StripHtmlTags(text);
        if (Helper.EstimateTextWidth(plainText) <= maxWidth)
        {
            return (text, -1);
        }

        // Extract all plain text characters from segments
        var segments = ParseHtmlSegments(text);
        var plainChars = segments
            .Where(s => !s.IsTag)
            .SelectMany(s => s.Content)
            .ToArray();

        if (plainChars.Length == 0)
        {
            return (text, -1);
        }

        // Calculate how many characters can fit
        var targetCharCount = CalculateTargetCharCount(plainChars, maxWidth);
        if (targetCharCount == 0)
        {
            return (string.Empty, -1);
        }

        // Calculate range to keep from middle
        var skipFromStart = Math.Max(0, (plainChars.Length - targetCharCount) / 2);
        var skipFromEnd = plainChars.Length - skipFromStart - targetCharCount;

        StringBuilder result = new();
        List<string> outputOpenTags = [];
        List<string> pendingOpenTags = [];
        var (plainCharIndex, hasStartedOutput) = (0, false);

        foreach (var (content, isTag) in segments)
        {
            switch (isTag, hasStartedOutput)
            {
                // Process tags after output has started
                case (true, true):
                    result = result.Append(content);
                    ProcessOpenTag(content, outputOpenTags);
                    break;

                // Queue opening tags before output starts
                case (true, false) when !content.StartsWith("</") && !content.StartsWith("<!") && !content.EndsWith("/>"):
                    pendingOpenTags.Add(content);
                    break;

                // Handle closing tags in skipped region, remove matching opening tag from pending
                case (true, false) when content.StartsWith("</"):
                    var closingTagName = content[2..^1].Split(' ')[0];
                    var matchIndex = pendingOpenTags.FindLastIndex(t =>
                        t[1..^1].Split(' ')[0].Equals(closingTagName, StringComparison.OrdinalIgnoreCase));
                    if (matchIndex >= 0)
                    {
                        pendingOpenTags.RemoveAt(matchIndex);
                    }
                    break;

                // Process plain text, keeping only middle portion
                case (false, _):
                    foreach (var ch in content)
                    {
                        if (plainCharIndex >= skipFromStart && plainCharIndex < plainChars.Length - skipFromEnd)
                        {
                            // Start output and apply pending tags
                            if (!hasStartedOutput)
                            {
                                hasStartedOutput = true;
                                pendingOpenTags.ForEach(tag =>
                                {
                                    result = result.Append(tag);
                                    ProcessOpenTag(tag, outputOpenTags);
                                });
                            }
                            result = result.Append(ch);
                        }
                        plainCharIndex++;
                    }
                    break;
                default:
                    break;
            }
        }

        CloseOpenTags(result, outputOpenTags);
        return (result.ToString(), -1);
    }

    /// <summary>
    /// Removes all HTML tags from the given text.
    /// </summary>
    /// <param name="text">The text containing HTML tags.</param>
    /// <returns>The text with all HTML tags removed.</returns>
    private static string StripHtmlTags( string text )
    {
        return string.IsNullOrEmpty(text) ? text : HtmlTagRegex().Replace(text, string.Empty);
    }

    /// <summary>
    /// Parses text into segments, separating HTML tags from plain text content.
    /// </summary>
    /// <param name="text">The text to parse.</param>
    /// <returns>A list of segments where each segment is either a tag or plain text content.</returns>
    private static List<(string Content, bool IsTag)> ParseHtmlSegments( string text )
    {
        var tagMatches = HtmlTagRegex().Matches(text);
        if (tagMatches.Count == 0)
        {
            return [(text, false)];
        }

        List<(string Content, bool IsTag)> segments = [];
        var currentIndex = 0;

        foreach (Match match in tagMatches)
        {
            if (match.Index > currentIndex)
            {
                segments.Add((text[currentIndex..match.Index], false));
            }
            segments.Add((match.Value, true));
            currentIndex = match.Index + match.Length;
        }

        if (currentIndex < text.Length)
        {
            segments.Add((text[currentIndex..], false));
        }

        return segments;
    }

    /// <summary>
    /// Processes an HTML tag and updates the list of currently open tags.
    /// Adds opening tags to the list and removes matching closing tags.
    /// </summary>
    /// <param name="tag">The HTML tag to process.</param>
    /// <param name="openTags">The list of currently open tag names.</param>
    private static void ProcessOpenTag( string tag, List<string> openTags )
    {
        var tagName = tag switch {
            ['<', '/', .. var rest] => new string(rest).TrimEnd('>').Split(' ', 2)[0],
            ['<', '!', ..] => null,
            [.. var chars] when chars[^1] == '/' && chars[^2] == '>' => null,
            ['<', .. var rest] => new string(rest).TrimEnd('>').Split(' ', 2)[0],
            _ => null
        };

        if (tagName == null)
        {
            return;
        }

        if (tag.StartsWith("</"))
        {
            var index = openTags.FindLastIndex(t => t.Equals(tagName, StringComparison.OrdinalIgnoreCase));
            if (index >= 0) openTags.RemoveAt(index);
        }
        else
        {
            openTags.Add(tagName);
        }
    }

    /// <summary>
    /// Appends closing tags for all currently open tags in reverse order.
    /// </summary>
    /// <param name="result">The StringBuilder to append closing tags to.</param>
    /// <param name="openTags">The list of currently open tag names.</param>
    private static void CloseOpenTags( StringBuilder result, List<string> openTags )
    {
        openTags.AsEnumerable().Reverse().ToList().ForEach(tag => result.Append($"</{tag}>"));
    }

    /// <summary>
    /// Calculates how many characters can fit within the specified width.
    /// </summary>
    /// <param name="plainChars">The characters to measure.</param>
    /// <param name="maxWidth">The maximum width allowed.</param>
    /// <returns>The number of characters that fit within the width.</returns>
    private static int CalculateTargetCharCount( ReadOnlySpan<char> plainChars, float maxWidth )
    {
        var currentWidth = 0f;
        var count = 0;

        foreach (var ch in plainChars)
        {
            var charWidth = Helper.GetCharWidth(ch);
            if (currentWidth + charWidth > maxWidth) break;
            currentWidth += charWidth;
            count++;
        }

        return count;
    }

    /// <summary>
    /// Updates and returns the scroll offset for the given text.
    /// The offset increments based on tick count and wraps around at the specified length.
    /// </summary>
    /// <param name="plainText">The plain text being scrolled.</param>
    /// <param name="scrollLeft">Whether scrolling left or right.</param>
    /// <param name="wrapLength">The length at which the offset wraps around.</param>
    /// <returns>The current scroll offset, or -1 if the text is not being scrolled.</returns>
    private int UpdateScrollOffset( string plainText, bool scrollLeft, int wrapLength )
    {
        var key = $"{plainText}_{scrollLeft}";

        var newOffset = scrollOffsets.TryGetValue(key, out var offset) ? (offset + 1) % wrapLength : 0;

        _ = scrollOffsets.AddOrUpdate(key, newOffset, ( _, _ ) => newOffset);
        return newOffset;
    }

    /// <summary>
    /// Updates the list of active tags based on the given HTML tag content.
    /// Adds opening tags and removes matching closing tags.
    /// </summary>
    /// <param name="content">The HTML tag content to process.</param>
    /// <param name="activeTags">The list of currently active tags.</param>
    private static void UpdateTagState( string content, List<string> activeTags )
    {
        if (!content.StartsWith("</") && !content.StartsWith("<!") && !content.EndsWith("/>"))
        {
            activeTags.Add(content);
        }
        else if (content.StartsWith("</"))
        {
            var tagName = content[2..^1].Split(' ')[0];
            var index = activeTags.FindLastIndex(t => t[1..^1].Split(' ')[0].Equals(tagName, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                activeTags.RemoveAt(index);
            }
        }
    }

    /// <summary>
    /// Prepares data required for text scrolling by extracting plain characters and parsing segments.
    /// </summary>
    /// <param name="text">The text to prepare for scrolling.</param>
    /// <param name="maxWidth">The maximum width available for display.</param>
    /// <returns>A tuple containing plain characters array, HTML segments, and target character count.</returns>
    private static (char[]? PlainChars, List<(string Content, bool IsTag)> Segments, int TargetCharCount) PrepareScrollData( string text, float maxWidth )
    {
        var plainText = StripHtmlTags(text);
        if (Helper.EstimateTextWidth(plainText) <= maxWidth)
        {
            return (null, [], 0);
        }

        var segments = ParseHtmlSegments(text);
        var plainChars = segments.Where(s => !s.IsTag).SelectMany(s => s.Content).ToArray();

        if (plainChars.Length == 0)
        {
            return (null, segments, 0);
        }

        var targetCharCount = CalculateTargetCharCount(plainChars, maxWidth);
        return (plainChars, segments, targetCharCount);
    }
}