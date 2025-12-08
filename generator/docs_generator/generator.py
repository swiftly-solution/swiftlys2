import os
import re
import yaml
import shutil


SOURCE_DIR = "../../api"
DEST_DIR = "../../docs"


os.makedirs(DEST_DIR, exist_ok=True)


def escape_generics(text):
    """Escape < and > in generic types for MDX compatibility."""
    if not text:
        return text
    return text.replace('<', '&lt;').replace('>', '&gt;')


def fix_html_for_jsx(text):
    """Fix HTML attributes for JSX compatibility."""
    if not text:
        return text
    # Replace class= with className=
    return text.replace('class=', 'className=')


def escape_generics_in_link_text(text):
    """Escape generics in markdown link text only."""
    if not text:
        return text
    return text.replace('<', '\\<').replace('>', '\\>')


def get_namespace(yaml_data):
    """Extract namespace from the YAML data."""
    for item in yaml_data.get('body', []):
        if 'facts' in item:
            for fact in item['facts']:
                if fact.get('name') == 'Namespace':
                    if isinstance(fact.get('value'), dict):
                        return fact['value'].get('text', '')
                    return fact.get('value', '')
    return ''


def extract_metadata(yaml_data, is_index=False):
    """Extract front-matter metadata from DocFX ApiPage YAML."""
    title = yaml_data.get('title', '')
    if not title:
        return {}

    # First remove generics and brackets from the full title
    clean_title = re.sub(r'<[^>]+>', '', title)
    clean_title = re.sub(r'\[[^\]]+\]', '', clean_title)

    if is_index:
        clean_title = clean_title.split(".")[-1]
    else:
        words = clean_title.split()
        clean_title = words[-1] if words else ''

    return {'title': clean_title}


def convert_to_path(s):
    """Convert DocFX URL to site path."""
    if s.lower().endswith(".html"):
        s = s[:-5]  # Remove .html first
    
    # Remove prefixes in order of specificity
    for prefix in ["SwiftlyS2.Core.", "SwiftlyS2.Shared.", "SwiftlyS2."]:
        if s.startswith(prefix):
            s = s[len(prefix):]
            break
    
    path = "/".join(s.split(".")).lower()
    
    # Transform -NUMBER to t repeated NUMBER times
    parts = path.split("/")
    parts[-1] = transform_filename(parts[-1])
    path = "/".join(parts)
    return path


def transform_filename(base_name):
    """
    If filename ends with -NUMBER, replace with NUMBER times 't'.
    Example: class-3 -> classttt
    """
    match = re.match(r"^(.*?)-(\d+)$", base_name)
    if match:
        name, num = match.groups()
        num = int(num)
        return name + ("t" * num)
    return base_name


def format_type_link(type_info):
    """Format type information as ApiParam component."""
    if isinstance(type_info, list):
        parts = []
        for t in type_info:
            if isinstance(t, dict):
                text = t.get('text', '')
                url = t.get('url', '')
                if url.endswith('.html'):
                    url = "/docs/api/" + convert_to_path(url)
                    url = url.replace('/api/shared/', '/api/').replace('/api/core/', '/api/')
                parts.append((text, url))
            else:
                parts.append((str(t), ''))
        type_text = ''.join([p[0] for p in parts])
        type_href = next((p[1] for p in parts if p[1]), '')
        return type_text, type_href
    elif isinstance(type_info, dict):
        text = type_info.get('text', '')
        url = type_info.get('url', '')
        if url.endswith('.html'):
            url = "/docs/api/" + convert_to_path(url)
            url = url.replace('/api/shared/', '/api/').replace('/api/core/', '/api/')
        return text, url
    return str(type_info), ''


def generate_markdown(yaml_data):
    """Generate MDX content from DocFX ApiPage YAML."""
    md = ""
    namespace = get_namespace(yaml_data)
    
    for item in yaml_data.get('body', []):
        if 'api1' in item:
            api1_title = str(item.get('api1', ''))
            api1_title = re.sub(r'<[^>]+>', '', api1_title)
            md += f"# {api1_title}\n\n"
            if 'src' in item:
                src = item['src'].replace('/blob/main', '/blob/master')
                md += f'<ViewSource href="{src}" />\n\n'
        
        if 'facts' in item:
            for fact in item['facts']:
                fact_name = fact.get('name', '')
                fact_value = fact.get('value', '')
                if isinstance(fact_value, dict):
                    fact_text = fact_value.get('text', '')
                    fact_url = fact_value.get('url', '')
                    if fact_url and fact_url.endswith('.html'):
                        fact_url = "/docs/api/" + convert_to_path(fact_url)
                        fact_url = fact_url.replace('/api/shared/', '/api/').replace('/api/core/', '/api/')
                        md += f"**{fact_name}**: [{escape_generics_in_link_text(fact_text)}]({fact_url})\n\n"
                    else:
                        md += f"**{fact_name}**: {fact_text}\n\n"
                else:
                    md += f"**{fact_name}**: {fact_value}\n\n"
        
        if 'markdown' in item:
            md += f"{item['markdown']}\n\n"
        
        if 'h2' in item:
            md += f"## {item['h2']}\n\n"
        
        if 'h4' in item:
            h4_text = item['h4']
            if h4_text in ['Parameters', 'Returns', 'Field Value', 'Property Value']:
                md += f"<ApiLabel>{h4_text}</ApiLabel>\n\n"
            else:
                md += f"#### {h4_text}\n\n"
        
        if 'code' in item:
            md += "```csharp\n" + item['code'] + "\n```\n\n"
        
        if 'inheritance' in item:
            for inherit in item['inheritance']:
                inherit_text = inherit.get('text', '')
                inherit_url = inherit.get('url', '')
                if inherit_url:
                    if inherit_url.endswith('.html'):
                        inherit_url = "/docs/api/" + convert_to_path(inherit_url)
                        inherit_url = inherit_url.replace('/api/shared/', '/api/').replace('/api/core/', '/api/')
                    md += f"- [{escape_generics_in_link_text(inherit_text)}]({inherit_url})\n"
                else:
                    md += f"- {inherit_text}\n"
            md += "\n"
        
        if 'list' in item:
            for list_item in item['list']:
                list_text = list_item.get('text', '')
                list_url = list_item.get('url', '')
                if list_url:
                    if list_url.endswith('.html'):
                        list_url = "/docs/api/" + convert_to_path(list_url)
                        list_url = list_url.replace('/api/shared/', '/api/').replace('/api/core/', '/api/')
                    md += f"- [{escape_generics_in_link_text(list_text)}]({list_url})\n"
                else:
                    md += f"- {list_text}\n"
            md += "\n"
        
        if 'parameters' in item:
            for param in item['parameters']:
                param_name = param.get('name', '')
                param_default = param.get('default', '')
                param_description = param.get('description', '')
                
                type_text, type_href = '', ''
                if 'type' in param:
                    type_text, type_href = format_type_link(param['type'])
                
                type_text_escaped = escape_generics(type_text)
                
                parts = []
                if param_name:
                    parts.append(f'name="{param_name}"')
                if type_text_escaped:
                    parts.append(f'type="{type_text_escaped}"')
                if type_href:
                    parts.append(f'typeHref="{type_href}"')
                
                api_param = f"<ApiParam {' '.join(parts)} />"
                
                if param_description:
                    md += f"- {api_param} — {fix_html_for_jsx(param_description)}\n"
                elif param_default != '':
                    md += f"- {api_param} = {param_default}\n"
                else:
                    md += f"- {api_param}\n"
            md += "\n"
        
        if 'api3' in item:
            src = item.get('src', '')
            api3_title = str(item.get('api3', ''))
            api3_title = re.sub(r'<[^>]+>', '', api3_title)
            api3_title = re.sub(r'\[[^\]]+\]', '', api3_title)
            md += f"### {api3_title}\n\n"
            if src != '':
                src = src.replace('/blob/main', '/blob/master')
                md += f'<ViewSource href="{src}" />\n\n'
    
    return md


def convert_yaml_file(src_path, dest_path):
    with open(src_path, 'r', encoding='utf-8') as f:
        yaml_data = yaml.safe_load(f)

    if isinstance(yaml_data, list):
        yaml_data = yaml_data[0]

    folder_path, file_name = os.path.split(dest_path)
    base_name, ext = os.path.splitext(file_name)
    is_index = (
        os.path.isdir(dest_path) or os.path.exists(os.path.join(folder_path, base_name))
    )

    metadata = extract_metadata(yaml_data, is_index=is_index)
    if metadata == {}:
        return
    
    md_content = "---\n"
    md_content += yaml.safe_dump(metadata, sort_keys=False)
    md_content += "---\n\n"
    md_content += generate_markdown(yaml_data)

    base_name = transform_filename(base_name)
    final_path = os.path.join(folder_path, base_name + ".mdx")

    if is_index:
        final_folder = os.path.join(folder_path, base_name)
        os.makedirs(final_folder, exist_ok=True)
        final_path = os.path.join(final_folder, "index.mdx")
    else:
        os.makedirs(folder_path, exist_ok=True)

    md_content = md_content.replace('/api/shared', '/api').replace('/api/core', '/api')

    with open(final_path, 'w', encoding='utf-8') as f:
        f.write(md_content)


for root, dirs, files in os.walk(SOURCE_DIR):
    for file in files:
        if file.endswith(".yml") or file.endswith(".yaml"):
            raw_base = os.path.splitext(file)[0]
            for prefix in ["SwiftlyS2.Core.", "SwiftlyS2.Shared.", "SwiftlyS2."]:
                if raw_base.startswith(prefix):
                    raw_base = raw_base[len(prefix):]
                    break
            new_base = transform_filename(raw_base)
            dest_file = os.path.join(DEST_DIR, "/".join(new_base.split(".")).lower() + ".mdx")
            convert_yaml_file(os.path.join(root, file), dest_file)

script_dir = os.path.dirname(os.path.abspath(__file__))
index_source = os.path.join(script_dir, "index.mdx")
index_dest = os.path.join(DEST_DIR, "index.mdx")

if os.path.exists(index_source):
    shutil.copy2(index_source, index_dest)
    print(f"Copied index.mdx to {index_dest}")
else:
    print(f"Warning: index.mdx not found at {index_source}")

print("MDX generation complete!")