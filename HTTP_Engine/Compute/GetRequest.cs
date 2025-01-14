/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using BH.oM.Base;
using System.Collections.Generic;
using System.Net.Http;
using BH.oM.Base.Attributes;

namespace BH.Engine.Adapters.HTTP
{
    public static partial class Compute
    {
        [ToBeRemoved("3.1", "Does not comply with BHoM Adaptor Push and Pull protocols")]
        public static string GetRequest(string uri)
        {
            using (HttpResponseMessage response = new HttpClient().GetAsync(uri).Result)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    Engine.Base.Compute.ClearCurrentEvents();
                    Engine.Base.Compute.RecordError($"GET request failed with code {response.StatusCode}: {response.ReasonPhrase}");
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        /***************************************************/

        private static string GetRequest(string uri, Dictionary<string, object> headers = null)
        {
            HttpClient client = new HttpClient();

            //Add headers
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value.ToString());
                }
            }

            using (HttpResponseMessage response = client.GetAsync(uri, HttpCompletionOption.ResponseContentRead).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Engine.Base.Compute.ClearCurrentEvents();
                    Engine.Base.Compute.RecordError($"GET request failed with code {response.StatusCode}: {response.ReasonPhrase}");
                    return null;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }

        /***************************************************/

        private static string GetRequest(string baseUrl, Dictionary<string, object> headers = null, Dictionary<string, object> parameters = null)
        {
            string uri = Convert.ToUrlString(baseUrl, parameters);
            return GetRequest(uri, headers);
        }

        /***************************************************/

        private static byte[] GetRequestBinary(string uri, Dictionary<string, object> headers = null)
        {
            HttpClient client = new HttpClient();

            //Add headers
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value.ToString());
                }
            }

            using (HttpResponseMessage response = client.GetAsync(uri, HttpCompletionOption.ResponseContentRead).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Engine.Base.Compute.ClearCurrentEvents();
                    Engine.Base.Compute.RecordError($"GET request failed with code {response.StatusCode}: {response.ReasonPhrase}");
                    return null;
                }
                else
                {
                    byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                    return result;
                }
            }
        }

        /***************************************************/

        private static byte[] GetRequestBinary(string baseUrl, Dictionary<string, object> headers = null, Dictionary<string, object> parameters = null)
        {
            string uri = Convert.ToUrlString(baseUrl, parameters);
            return GetRequestBinary(uri, headers);
        }

        /***************************************************/

    }
}



