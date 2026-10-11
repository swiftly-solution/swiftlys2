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

#ifndef src_extension_extension_h
#define src_extension_extension_h

#include <string>
#include <api/extension/sw_extension.h>

enum class ExtensionLoadState
{
    Loading,
    Loaded,
    Failed
};

class Extension
{
public:
    Extension(const std::string& id, const std::string& path, void* library);
    ~Extension();
    Extension(const Extension&) = delete;
    Extension& operator=(const Extension&) = delete;

    virtual void Init(const char* name, const char* version, const char* author, const char* description);
    virtual bool IsInitialized();
    virtual void* GetCore(const char* core_name);
    virtual void SetHotReloaded(bool hotreloaded);
    virtual bool IsHotReloaded();

    bool IsInitializing() const { return initializing_; }
    void FinishInitialization();
    void SetUnloadCallback(sw_extension_callback_fn callback, void* user_data);
    void SetOnAllExtensionsLoadedCallback(sw_extension_callback_fn callback, void* user_data);
    void OnAllExtensionsLoaded();

    const std::string& GetId() const { return id_; }
    const std::string& GetPath() const { return path_; }
    const std::string& GetName() const { return name_; }
    const std::string& GetVersion() const { return version_; }
    const std::string& GetAuthor() const { return author_; }
    const std::string& GetDescription() const { return description_; }
    void* GetLibrary() const { return library_; }
private:
    bool initialized_;
    std::string id_;
    std::string path_;
    std::string name_;
    std::string version_;
    std::string author_;
    std::string description_;
    void* library_;
    void* core_;
    bool hot_reloaded_;
    bool initializing_;
    sw_extension_callback_fn unload_callback_;
    void* unload_user_data_;
    sw_extension_callback_fn on_all_extensions_loaded_callback_;
    void* on_all_extensions_loaded_user_data_;
};

#endif
