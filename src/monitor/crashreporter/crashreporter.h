/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#ifndef src_monitor_crashreporter_crashreporter_h
#define src_monitor_crashreporter_crashreporter_h

#include <api/monitor/crashreporter/crashreporter.h>

class CrashReporter : public ICrashReporter
{
private:
    int m_tracerLevel;
public:
    virtual void Init() override;
    virtual void Shutdown() override;

    virtual void EnableDotnetCrashTracer(int level) override;
    virtual int GetDotnetCrashTracerLevel() override;
    virtual void ReportPreventionIncident(std::string category, std::string reason) override;
};

#endif