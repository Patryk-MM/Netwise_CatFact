using CatFact.Services;

namespace CatFact;

public class MainMenu {
	private readonly ICatFactService _service;

	public MainMenu(ICatFactService service) {
		_service = service;
	}


	public async Task RunAsync() {
		try {
			var fact = await _service.FetchFact();
			await _service.WriteToFile(fact);
		}
		catch (Exception ex) {
			Console.WriteLine(ex.Message);
		}
	}
}
