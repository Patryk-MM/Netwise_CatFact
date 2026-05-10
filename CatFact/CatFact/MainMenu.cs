using CatFact.Services;

namespace CatFact;

public class MainMenu {
	private readonly CatFactService _service;

	public MainMenu(CatFactService service) {
		_service = service;
	}


	public async Task RunAsync() {
		await _service.FetchFact();
		await Task.Delay(1500);
	}
}
