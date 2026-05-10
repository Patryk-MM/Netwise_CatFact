using System.Net.Http.Json;
using CatFact.Models;

namespace CatFact.Services;

public class CatFactService {

	private readonly HttpClient _httpClient;

	public CatFactService(HttpClient httpClient) {
		_httpClient = httpClient;
	}


	public async Task FetchFact() {
		var result = await _httpClient.GetFromJsonAsync<CatFactDTO>("");
		Console.WriteLine(result?.Fact);
	}
}
