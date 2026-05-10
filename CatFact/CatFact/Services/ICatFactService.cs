using CatFact.Models;

namespace CatFact.Services;

public interface ICatFactService {
	Task<CatFactDTO?> FetchFact();
	Task WriteToFile(CatFactDTO fact);
	Task<string[]> ReadFromFile();
}
