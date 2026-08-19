/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#ifndef src_api_utils_bitvec_h
#define src_api_utils_bitvec_h

#include <cstdint>
#include <algorithm>

template<int N>
class CBitVector
{
public:
    bool IsBitSet(int bit)
    {
        return (m_uData[bit >> 6] & (1ULL << (bit & 63))) != 0;
    }

    void Set(int bit)
    {
        m_uData[bit >> 6] |= (1ULL << (bit & 63));
    }

    uint64_t GetQWord(int qword)
    {
        return m_uData[qword];
    }

    void Clear(int bit)
    {
        m_uData[bit >> 6] &= ~(1ULL << (bit & 63));
    }

    void ClearAll()
    {
        std::fill(std::begin(m_uData), std::end(m_uData), 0ULL);
    }

    void Filter(CBitVector<N>& filter)
    {
        for (int i = 0; i < NUM_QWORDS; i++)
        {
            auto qword_data = filter.GetQWord(i);
            if (qword_data != 0) m_uData[i] &= ~qword_data;
        }
    }

private:
    enum
    {
        NUM_QWORDS = (N + 63) >> 6
    };

    uint64_t m_uData[NUM_QWORDS];
};

#endif