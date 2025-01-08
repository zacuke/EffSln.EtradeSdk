//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 649 // Disable "CS0649 Field is never assigned to, and will always have its default value null"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"
#pragma warning disable 8625 // Disable "CS8625 Cannot convert null literal to non-nullable reference type"
#pragma warning disable 8765 // Disable "CS8765 Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes)."

namespace EffSln.EtradeSdk.Accounts
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AccountsClient 
    {
        #pragma warning disable 8618
        private string _baseUrl;
        #pragma warning restore 8618

        private System.Net.Http.HttpClient _httpClient;
        private static System.Lazy<System.Text.Json.JsonSerializerOptions> _settings = new System.Lazy<System.Text.Json.JsonSerializerOptions>(CreateSerializerSettings, true);
        private System.Text.Json.JsonSerializerOptions _instanceSettings;

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AccountsClient(System.Net.Http.HttpClient httpClient)
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            BaseUrl = "https://apisb.etrade.com/v1/accounts";
            _httpClient = httpClient;
            Initialize();
        }

        private static System.Text.Json.JsonSerializerOptions CreateSerializerSettings()
        {
            var settings = new System.Text.Json.JsonSerializerOptions();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
                if (!string.IsNullOrEmpty(_baseUrl) && !_baseUrl.EndsWith("/"))
                    _baseUrl += '/';
            }
        }

        protected System.Text.Json.JsonSerializerOptions JsonSerializerSettings { get { return _instanceSettings ?? _settings.Value; } }

        static partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings);

        partial void Initialize();

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>
        /// List Accounts
        /// </summary>
        /// <remarks>
        /// Returns a list of E*TRADE accounts for the current user.
        /// </remarks>
        /// <param name="oauth_token">Oauth_token received from the Get Access Token API.</param>
        /// <param name="oauth_token_secret">Oauth_token_secret received from the Get Access Token API.</param>
        /// <returns>Successful operation</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public virtual System.Threading.Tasks.Task<AccountListResponse> ListAccountsAsync(string oauth_token, string oauth_token_secret)
        {
            return ListAccountsAsync(oauth_token, oauth_token_secret, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// List Accounts
        /// </summary>
        /// <remarks>
        /// Returns a list of E*TRADE accounts for the current user.
        /// </remarks>
        /// <param name="oauth_token">Oauth_token received from the Get Access Token API.</param>
        /// <param name="oauth_token_secret">Oauth_token_secret received from the Get Access Token API.</param>
        /// <returns>Successful operation</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<AccountListResponse> ListAccountsAsync(string oauth_token, string oauth_token_secret, System.Threading.CancellationToken cancellationToken)
        {
            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {

                    if (oauth_token == null)
                        throw new System.ArgumentNullException("oauth_token");
                    request_.Headers.TryAddWithoutValidation("oauth_token", ConvertToString(oauth_token, System.Globalization.CultureInfo.InvariantCulture));

                    if (oauth_token_secret == null)
                        throw new System.ArgumentNullException("oauth_token_secret");
                    request_.Headers.TryAddWithoutValidation("oauth_token_secret", ConvertToString(oauth_token_secret, System.Globalization.CultureInfo.InvariantCulture));
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/xml"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "list"
                    urlBuilder_.Append("list");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AccountListResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ == 204)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<object>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<object>("No records available", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 500)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<object>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<object>("Internal server error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; } = true;

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                responseText = ConvertUrlFormToJson(responseText);
                try
                {
                    var typedBody = System.Text.Json.JsonSerializer.Deserialize<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (System.Text.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        var typedBody = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(responseStream, JsonSerializerSettings, cancellationToken).ConfigureAwait(false);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (System.Text.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertUrlFormToJson(string input)
        {

            if (input.Contains("<?xml version=\"1.0\"")){

                var xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(input);

                var jsonCompatible = ConvertToDictionary(xmlDoc.DocumentElement);

                // Serialize Dictionary to JSON
                string json = System.Text.Json.JsonSerializer.Serialize(jsonCompatible, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                return json;
                 
            }
            else
            {
                // Parse the response into a collection using HttpUtility
                var queryParams = System.Web.HttpUtility.ParseQueryString(input);

                // Create a dictionary to hold the parsed values
                var dictionary = new System.Collections.Generic.Dictionary<string, string>();
                foreach (string key in queryParams.Keys)
                {
                    dictionary[key] = queryParams[key];
                } 
                return System.Text.Json.JsonSerializer.Serialize(dictionary);
            }
           
        }
        static object ConvertToDictionary(System.Xml.XmlNode node)
        {
            if (node.ChildNodes.Count == 1 && node.FirstChild is System.Xml.XmlText)
            {
                // Base case: node contains a single text node (leaf node)
                return node.InnerText;
            }
            else
            {
                // Create a dictionary to store child nodes (or their arrays)
                var dict = new System.Collections.Generic.Dictionary<string, object>();

                foreach (System.Xml.XmlNode child in node.ChildNodes)
                {
                    if (!dict.ContainsKey(child.Name))
                    {
                        // Turn each unique child name into a list to support repeating elements.
                        dict[child.Name] = new System.Collections.Generic.List<object>();
                    }

                    var list = dict[child.Name] as System.Collections.Generic.List<object>;
                    list.Add(ConvertToDictionary(child)); // Recursive call
                }

                // For repeating children (e.g., multiple Accounts nodes) return the list as a value
                foreach (var key in dict.Keys)
                {
                    var list = dict[key] as System.Collections.Generic.List<object>;
                    if (list.Count == 1)
                    {
                        // Simplify single-element lists to just the element
                        dict[key] = list[0];
                    }
                }

                return dict;
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute)) 
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool) 
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[]) value);
            }
            else if (value is string[])
            {
                return string.Join(",", (string[])value);
            }
            else if (value.GetType().IsArray)
            {
                var valueArray = (System.Array)value;
                var valueTextArray = new string[valueArray.Length];
                for (var i = 0; i < valueArray.Length; i++)
                {
                    valueTextArray[i] = ConvertToString(valueArray.GetValue(i), cultureInfo);
                }
                return string.Join(",", valueTextArray);
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }
    }

    /// <summary>
    /// Response object containing an array of accounts.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AccountListResponse
    {
        /// <summary>
        /// Contains the list of accounts.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("Accounts")]
        public Accounts Accounts { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

        [System.Text.Json.Serialization.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

    /// <summary>
    /// Details of a single account.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Account
    {
        /// <summary>
        /// The user's account ID.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountId")]
        public string AccountId { get; set; }

        /// <summary>
        /// The unique account key.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountIdKey")]
        public string AccountIdKey { get; set; }

        /// <summary>
        /// The mode of the account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountMode")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public AccountMode AccountMode { get; set; }

        /// <summary>
        /// Description of the account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountDesc")]
        public string AccountDesc { get; set; }

        /// <summary>
        /// The nickname for the account. Can be empty.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountName")]
        public string AccountName { get; set; }

        /// <summary>
        /// The account type.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountType")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public AccountType AccountType { get; set; }

        /// <summary>
        /// The institution type of the account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("institutionType")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public AccountInstitutionType InstitutionType { get; set; }

        /// <summary>
        /// The status of the account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("accountStatus")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public AccountStatus AccountStatus { get; set; }

        /// <summary>
        /// The date when the account was closed.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("closedDate")]
        public long ClosedDate { get; set; }

        /// <summary>
        /// Indicates if it is a Shareworks Account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("shareWorksAccount")]
        public bool ShareWorksAccount { get; set; }

        /// <summary>
        /// The source of the Shareworks account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("shareWorksSource")]
        public string ShareWorksSource { get; set; }

        /// <summary>
        /// Indicates if the account is a managed MSSB closed account.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("fcManagedMssbClosedAccount")]
        public bool FcManagedMssbClosedAccount { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

        [System.Text.Json.Serialization.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Accounts
    {
        /// <summary>
        /// List of accounts.
        /// </summary>

        [System.Text.Json.Serialization.JsonPropertyName("Account")]
        public System.Collections.Generic.ICollection<Account> Account { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

        [System.Text.Json.Serialization.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum AccountMode
    {

        [System.Runtime.Serialization.EnumMember(Value = @"CASH")]
        CASH = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"MARGIN")]
        MARGIN = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"CHECKING")]
        CHECKING = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"IRA")]
        IRA = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"SAVINGS")]
        SAVINGS = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"CD")]
        CD = 5,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum AccountType
    {

        [System.Runtime.Serialization.EnumMember(Value = @"AMMCHK")]
        AMMCHK = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"ARO")]
        ARO = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"BCHK")]
        BCHK = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"BENFIRA")]
        BENFIRA = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"BENFROTHIRA")]
        BENFROTHIRA = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"BENF_ESTATE_IRA")]
        BENF_ESTATE_IRA = 5,

        [System.Runtime.Serialization.EnumMember(Value = @"BENF_MINOR_IRA")]
        BENF_MINOR_IRA = 6,

        [System.Runtime.Serialization.EnumMember(Value = @"BENF_ROTH_ESTATE_IRA")]
        BENF_ROTH_ESTATE_IRA = 7,

        [System.Runtime.Serialization.EnumMember(Value = @"BENF_ROTH_MINOR_IRA")]
        BENF_ROTH_MINOR_IRA = 8,

        [System.Runtime.Serialization.EnumMember(Value = @"BENF_ROTH_TRUST_IRA")]
        BENF_ROTH_TRUST_IRA = 9,

        [System.Runtime.Serialization.EnumMember(Value = @"BENF_TRUST_IRA")]
        BENF_TRUST_IRA = 10,

        [System.Runtime.Serialization.EnumMember(Value = @"BRKCD")]
        BRKCD = 11,

        [System.Runtime.Serialization.EnumMember(Value = @"BROKER")]
        BROKER = 12,

        [System.Runtime.Serialization.EnumMember(Value = @"CASH")]
        CASH = 13,

        [System.Runtime.Serialization.EnumMember(Value = @"C_CORP")]
        C_CORP = 14,

        [System.Runtime.Serialization.EnumMember(Value = @"CONTRIBUTORY")]
        CONTRIBUTORY = 15,

        [System.Runtime.Serialization.EnumMember(Value = @"COVERDELL_ESA")]
        COVERDELL_ESA = 16,

        [System.Runtime.Serialization.EnumMember(Value = @"CONVERSION_ROTH_IRA")]
        CONVERSION_ROTH_IRA = 17,

        [System.Runtime.Serialization.EnumMember(Value = @"CREDITCARD")]
        CREDITCARD = 18,

        [System.Runtime.Serialization.EnumMember(Value = @"COMM_PROP")]
        COMM_PROP = 19,

        [System.Runtime.Serialization.EnumMember(Value = @"CONSERVATOR")]
        CONSERVATOR = 20,

        [System.Runtime.Serialization.EnumMember(Value = @"CORPORATION")]
        CORPORATION = 21,

        [System.Runtime.Serialization.EnumMember(Value = @"CSA")]
        CSA = 22,

        [System.Runtime.Serialization.EnumMember(Value = @"CUSTODIAL")]
        CUSTODIAL = 23,

        [System.Runtime.Serialization.EnumMember(Value = @"DVP")]
        DVP = 24,

        [System.Runtime.Serialization.EnumMember(Value = @"ESTATE")]
        ESTATE = 25,

        [System.Runtime.Serialization.EnumMember(Value = @"EMPCHK")]
        EMPCHK = 26,

        [System.Runtime.Serialization.EnumMember(Value = @"EMPMMCA")]
        EMPMMCA = 27,

        [System.Runtime.Serialization.EnumMember(Value = @"ETCHK")]
        ETCHK = 28,

        [System.Runtime.Serialization.EnumMember(Value = @"ETMMCHK")]
        ETMMCHK = 29,

        [System.Runtime.Serialization.EnumMember(Value = @"HEIL")]
        HEIL = 30,

        [System.Runtime.Serialization.EnumMember(Value = @"HELOC")]
        HELOC = 31,

        [System.Runtime.Serialization.EnumMember(Value = @"INDCHK")]
        INDCHK = 32,

        [System.Runtime.Serialization.EnumMember(Value = @"INDIVIDUAL")]
        INDIVIDUAL = 33,

        [System.Runtime.Serialization.EnumMember(Value = @"INDIVIDUAL_K")]
        INDIVIDUAL_K = 34,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB")]
        INVCLUB = 35,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_C_CORP")]
        INVCLUB_C_CORP = 36,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_LLC_C_CORP")]
        INVCLUB_LLC_C_CORP = 37,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_LLC_PARTNERSHIP")]
        INVCLUB_LLC_PARTNERSHIP = 38,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_LLC_S_CORP")]
        INVCLUB_LLC_S_CORP = 39,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_PARTNERSHIP")]
        INVCLUB_PARTNERSHIP = 40,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_S_CORP")]
        INVCLUB_S_CORP = 41,

        [System.Runtime.Serialization.EnumMember(Value = @"INVCLUB_TRUST")]
        INVCLUB_TRUST = 42,

        [System.Runtime.Serialization.EnumMember(Value = @"IRA_ROLLOVER")]
        IRA_ROLLOVER = 43,

        [System.Runtime.Serialization.EnumMember(Value = @"JOINT")]
        JOINT = 44,

        [System.Runtime.Serialization.EnumMember(Value = @"JTTEN")]
        JTTEN = 45,

        [System.Runtime.Serialization.EnumMember(Value = @"JTWROS")]
        JTWROS = 46,

        [System.Runtime.Serialization.EnumMember(Value = @"LLC_C_CORP")]
        LLC_C_CORP = 47,

        [System.Runtime.Serialization.EnumMember(Value = @"LLC_PARTNERSHIP")]
        LLC_PARTNERSHIP = 48,

        [System.Runtime.Serialization.EnumMember(Value = @"LLC_S_CORP")]
        LLC_S_CORP = 49,

        [System.Runtime.Serialization.EnumMember(Value = @"LLP")]
        LLP = 50,

        [System.Runtime.Serialization.EnumMember(Value = @"LLP_C_CORP")]
        LLP_C_CORP = 51,

        [System.Runtime.Serialization.EnumMember(Value = @"LLP_S_CORP")]
        LLP_S_CORP = 52,

        [System.Runtime.Serialization.EnumMember(Value = @"IRA")]
        IRA = 53,

        [System.Runtime.Serialization.EnumMember(Value = @"IRACD")]
        IRACD = 54,

        [System.Runtime.Serialization.EnumMember(Value = @"MONEY_PURCHASE")]
        MONEY_PURCHASE = 55,

        [System.Runtime.Serialization.EnumMember(Value = @"MARGIN")]
        MARGIN = 56,

        [System.Runtime.Serialization.EnumMember(Value = @"MRCHK")]
        MRCHK = 57,

        [System.Runtime.Serialization.EnumMember(Value = @"MUTUAL_FUND")]
        MUTUAL_FUND = 58,

        [System.Runtime.Serialization.EnumMember(Value = @"NONCUSTODIAL")]
        NONCUSTODIAL = 59,

        [System.Runtime.Serialization.EnumMember(Value = @"NON_PROFIT")]
        NON_PROFIT = 60,

        [System.Runtime.Serialization.EnumMember(Value = @"OTHER")]
        OTHER = 61,

        [System.Runtime.Serialization.EnumMember(Value = @"PARTNER")]
        PARTNER = 62,

        [System.Runtime.Serialization.EnumMember(Value = @"PARTNERSHIP")]
        PARTNERSHIP = 63,

        [System.Runtime.Serialization.EnumMember(Value = @"PARTNERSHIP_C_CORP")]
        PARTNERSHIP_C_CORP = 64,

        [System.Runtime.Serialization.EnumMember(Value = @"PARTNERSHIP_S_CORP")]
        PARTNERSHIP_S_CORP = 65,

        [System.Runtime.Serialization.EnumMember(Value = @"PDT_ACCOUNT")]
        PDT_ACCOUNT = 66,

        [System.Runtime.Serialization.EnumMember(Value = @"PM_ACCOUNT")]
        PM_ACCOUNT = 67,

        [System.Runtime.Serialization.EnumMember(Value = @"PREFCD")]
        PREFCD = 68,

        [System.Runtime.Serialization.EnumMember(Value = @"PREFIRACD")]
        PREFIRACD = 69,

        [System.Runtime.Serialization.EnumMember(Value = @"PROFIT_SHARING")]
        PROFIT_SHARING = 70,

        [System.Runtime.Serialization.EnumMember(Value = @"PROPRIETARY")]
        PROPRIETARY = 71,

        [System.Runtime.Serialization.EnumMember(Value = @"REGCD")]
        REGCD = 72,

        [System.Runtime.Serialization.EnumMember(Value = @"ROTHIRA")]
        ROTHIRA = 73,

        [System.Runtime.Serialization.EnumMember(Value = @"ROTH_INDIVIDUAL_K")]
        ROTH_INDIVIDUAL_K = 74,

        [System.Runtime.Serialization.EnumMember(Value = @"ROTH_IRA_MINORS")]
        ROTH_IRA_MINORS = 75,

        [System.Runtime.Serialization.EnumMember(Value = @"SARSEPIRA")]
        SARSEPIRA = 76,

        [System.Runtime.Serialization.EnumMember(Value = @"S_CORP")]
        S_CORP = 77,

        [System.Runtime.Serialization.EnumMember(Value = @"SEPIRA")]
        SEPIRA = 78,

        [System.Runtime.Serialization.EnumMember(Value = @"SIMPLE_IRA")]
        SIMPLE_IRA = 79,

        [System.Runtime.Serialization.EnumMember(Value = @"TIC")]
        TIC = 80,

        [System.Runtime.Serialization.EnumMember(Value = @"TRD_IRA_MINORS")]
        TRD_IRA_MINORS = 81,

        [System.Runtime.Serialization.EnumMember(Value = @"TRUST")]
        TRUST = 82,

        [System.Runtime.Serialization.EnumMember(Value = @"VARCD")]
        VARCD = 83,

        [System.Runtime.Serialization.EnumMember(Value = @"VARIRACD")]
        VARIRACD = 84,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum AccountInstitutionType
    {

        [System.Runtime.Serialization.EnumMember(Value = @"BROKERAGE")]
        BROKERAGE = 0,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum AccountStatus
    {

        [System.Runtime.Serialization.EnumMember(Value = @"ACTIVE")]
        ACTIVE = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"CLOSED")]
        CLOSED = 1,

    }



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604
#pragma warning restore 8625