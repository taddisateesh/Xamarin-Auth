using System;
using Newtonsoft.Json;

namespace SampleKT
{
	public class UserDetailsGoogleDto
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("verified_email")]
		public bool Verified_Email { get; set; }


		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("given_name")]
		public string Given_Name { get; set; }

		[JsonProperty("family_name")]
		public string Family_Name { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("picture")]
		public string Picture { get; set; }

		[JsonProperty("gender")]
		public string Gender { get; set; }
	}
}
