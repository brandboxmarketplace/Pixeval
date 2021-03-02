#region Copyright (C) 2019-2020 Dylech30th. All rights reserved.
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

using System.Threading.Tasks;
using Mako.Net.RequestModel;
using Mako.Net.ResponseModel;
using Refit;

namespace Mako.Net.Protocol
{
    [Headers("User-Agent: PixivAndroidApp/5.0.64 (Android 6.0)", "Content-Type: application/x-www-form-urlencoded")]
    public interface IAuthProtocol
    {
        [Post("/auth/token")]
        Task<TokenResponse> GetTokenByPassword([Body(BodySerializationMethod.UrlEncoded)] PasswordTokenRequest body, [Header("X-Client-Time")] string clientTime, [Header("X-Client-Hash")] string clientHash);

        [Post("/auth/token")]
        Task<TokenResponse> RefreshToken([Body(BodySerializationMethod.UrlEncoded)] RefreshTokenRequest body);
    }
}