using CatFact.Models;
using System.Net.Http.Json;

namespace CatFact.Services;

public class CatFactService : ICatFactService {

	private readonly HttpClient _httpClient;
	private const string path = "CatFacts.txt";

	public CatFactService(HttpClient httpClient) {
		_httpClient = httpClient;
	}

	public async Task<CatFactDTO?> FetchFact() {
		var result = await _httpClient.GetFromJsonAsync<CatFactDTO>("");
		return result;
	}

	public async Task WriteToFile(CatFactDTO fact) {
		await File.AppendAllTextAsync(path, $"[{fact.Length}] {fact.Fact}{Environment.NewLine}");
	}

	public async Task<string[]> ReadFromFile(){
		return await File.ReadAllLinesAsync(path);
	}
}
