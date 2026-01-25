using Master.Data.Core.RequestResponse.Common.Requests;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Master.Data.Core.RequestResponse.Common.Extensions;

public static class ResponseExtensions
{
    public static async Task<Response<T>> ToResultAsync<T>(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return Response.Fail<T>(response.ReasonPhrase);
            }

            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<T>(responseAsString);

            return Response.Ok(responseObject);
        }
        else
        {
            var responseAsString = await response.Content.ReadAsStringAsync();

            try
            {
                var responseObject = JsonConvert.DeserializeObject<FailResponse>(responseAsString);

                StringBuilder builder = new();

                foreach (var e in responseObject.Errors)
                {
                    builder.AppendFormat($"{string.Join(',', e.Value)}");
                    builder.AppendLine("\n");
                }

                return Response.Fail<T>(builder.ToString());
            }
            catch (Exception)
            {
                try
                {
                    var responseObject = JsonConvert.DeserializeObject<string[]>(responseAsString);

                    StringBuilder builder = new();

                    foreach (var e in responseObject)
                    {
                        builder.AppendFormat($"{string.Join(',', e)}");
                        builder.AppendLine("\n");
                    }

                    return Response.Fail<T>(builder.ToString());

                }
                catch (Exception)
                {
                    try
                    {
                        var responseObject = JsonConvert.DeserializeObject<string>(responseAsString);

                        return Response.Fail<T>(responseObject ?? response.ReasonPhrase);
                    }
                    catch (Exception)
                    {
                        return Response.Fail<T>(response.ReasonPhrase ?? responseAsString);
                    }
                }
            }
        }
    }

    public static Response ToResult(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return Response.Ok();
        }
        else
        {
            var responseAsString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            StringBuilder builder = new();
            try
            {
                var responseObject = JsonConvert.DeserializeObject<FailResponse>(responseAsString);


                foreach (var e in responseObject.Errors)
                {
                    builder.AppendFormat($"{string.Join(',', e.Value)} ");
                    builder.Append("\n");
                }

                return Response.Fail(builder.ToString());
            }
            catch (Exception)
            {
                try
                {
                    var responseObject = JsonConvert.DeserializeObject<string[]>(responseAsString);
                    builder.AppendJoin(",", responseObject);
                    return Response.Fail(builder.ToString());
                }
                catch (Exception)
                {

                    return Response.Fail(response.ReasonPhrase);
                }
            }
        }
    }

    public static bool IsEmpty<T>(this IEnumerable<T> ts)
    {
        if (ts == null || !ts.Any())
        {
            return true;
        }
        return false;
    }

    public static bool IsNotEmpty<T>(this IEnumerable<T> ts)
    {
        if (ts == null)
        {
            return false;
        }
        if (ts.Any())
        {
            return true;
        }
        return false;
    }

    public static bool IsNull(this object obj)
    {
        return obj == null;
    }

    public static bool IsNotNull(this object obj)
    {
        return obj != null;
    }

    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsNotNullOrEmpty(this string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    public static bool IsNotNullOrWhiteSpace(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }
}

