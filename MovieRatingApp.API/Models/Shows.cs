using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Shows
{
    public Metadata metadata { get; set; }
    public Result[] results { get; set; }
    public bool has_more { get; set; }
    public object slug { get; set; }
    public object display_name { get; set; }
}

public class Metadata
{
    public object content_kind { get; set; }
    public object display_name { get; set; }
    public string page_description { get; set; }
    public object page_subtext { get; set; }
    public object row_subtext { get; set; }
    public object ad_data { get; set; }
    public object[] disabled_filters { get; set; }
}

public class Result
{
    public string id { get; set; }
    public string slug { get; set; }
    public string content_type { get; set; }
    public string title { get; set; }
    public string overview { get; set; }
    public string availability_pros { get; set; }
    public float imdb_rating { get; set; }
    public int rt_critics_rating { get; set; }
    public float rg_content_score { get; set; }
    public bool has_poster { get; set; }
    public bool has_backdrop { get; set; }
    public DateTime released_on { get; set; }
    public string classification { get; set; }
    public string[] sources { get; set; }
    public int[] genres { get; set; }
    public bool on_services { get; set; }
    public bool on_free { get; set; }
    public bool on_rent_purchase { get; set; }
    public object user_rating { get; set; }
    public bool tracking { get; set; }
    public bool watchlisted { get; set; }
    public bool seen { get; set; }
    public int season_count { get; set; }
    public object[] featured_services { get; set; }
    public int episode_source_count { get; set; }

    [JsonIgnore]
    public string ImageUrl { get; set; }
}

public class MovieDetails
{
    public string id { get; set; }
    public string slug { get; set; }
    public string title { get; set; }
    public string overview { get; set; }
    public string tagline { get; set; }
    public string reelgood_synopsis { get; set; }
    public string classification { get; set; }
    public int runtime { get; set; }
    public string language { get; set; }
    public bool has_poster { get; set; }
    public bool has_backdrop { get; set; }
    public double imdb_rating { get; set; }
    public object rt_critics_rating { get; set; }
    public int rt_audience_rating { get; set; }
    public object user_rating { get; set; }
    public bool watchlisted { get; set; }
    public bool seen { get; set; }
    public object user_lists { get; set; }
    public bool on_free { get; set; }
    public bool on_rent_purchase { get; set; }
    public IList<Person> people { get; set; }
}

public class Person
{
    public string id { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public DateTime? birthdate { get; set; }
    public bool has_poster { get; set; }
    public bool has_square { get; set; }
    public int role_type { get; set; }
    public string role { get; set; }
    public int? rank { get; set; }
}
