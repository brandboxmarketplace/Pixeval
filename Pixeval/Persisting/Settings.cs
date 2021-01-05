﻿#region Copyright (C) 2019-2020 Dylech30th. All rights reserved.

// Pixeval - A Strong, Fast and Flexible Pixiv Client
// Copyright (C) 2019-2020 Dylech30th
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pixeval.Core;
using Pixeval.Objects.Primitive;
using PropertyChanged;

namespace Pixeval.Persisting
{
    /// <summary>
    ///     A class represents user preference
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Settings
    {
        [JsonIgnore]
        public static Settings Global = new Settings();

        private string downloadLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public bool SortOnInserting { get; set; }

        public int MinBookmark { get; set; } = 1;

        public bool RecommendIllustrator { get; set; }

        public string DownloadLocation
        {
            get => downloadLocation.IsNullOrEmpty() ? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) : downloadLocation;
            set => downloadLocation = value;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public SearchTagMatchOption TagMatchOption { get; set; } = SearchTagMatchOption.PartialMatchForTags;

        public string Culture { get; set; } = "zh-cn";

        public int QueryPages { get; set; } = 1;

        public int QueryStart { get; set; } = 1;

        public bool DirectConnect { get; set; }

        public int SpotlightQueryStart { get; set; } = 1;

        public ISet<string> ExcludeTag { get; set; } = new HashSet<string>();

        public ISet<string> IncludeTag { get; set; } = new HashSet<string>();

        public bool CreateNewFolderWhenDownloadFromUser { get; set; }

        public string Cookie { get; set; }

        public string MirrorServer { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }

        public async Task Save()
        {
            await File.WriteAllTextAsync(Path.Combine(PixevalContext.SettingsFolder, "settings.json"), Global.ToString());
        }

        public static async Task Load()
        {
            if (File.Exists(Path.Combine(PixevalContext.SettingsFolder, "settings.json")))
            {
                Global = (await File.ReadAllTextAsync(Path.Combine(PixevalContext.SettingsFolder, "settings.json"))).FromJson<Settings>();
            }
            else
            {
                Reset();
            }
        }

        public static void Reset()
        {
            File.Delete(Path.Combine(PixevalContext.SettingsFolder, "settings.json"));

            Global.downloadLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Global.DownloadLocation = string.Empty;
            Global.SortOnInserting = false;
            Global.IncludeTag = new HashSet<string>();
            Global.ExcludeTag = new HashSet<string>();
            Global.DirectConnect = false;
            Global.MinBookmark = 0;
            Global.QueryPages = 1;
            Global.QueryStart = 1;
            Global.SpotlightQueryStart = 1;
            Global.RecommendIllustrator = false;
            Global.CreateNewFolderWhenDownloadFromUser = false;
            Global.TagMatchOption = SearchTagMatchOption.PartialMatchForTags;
            Global.Culture = "zh-cn";
            Global.Cookie = string.Empty;
            Global.MirrorServer = string.Empty;
        }
    }
}