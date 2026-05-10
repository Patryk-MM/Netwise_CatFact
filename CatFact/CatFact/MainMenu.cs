using CatFact.Models;
using CatFact.Services;
using Spectre.Console;
using Spectre.Console.Json;
using System.Text.Json;

namespace CatFact;

public class MainMenu {
	private readonly ICatFactService _service;

	public MainMenu(ICatFactService service) {
		_service = service;
	}


	public async Task RunAsync() {
		while (true) {
			ClearConsole();

			var selected = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
				.Title("[bold][#F5EFE2]What do you want to do?[/][/]")
				.HighlightStyle(new Style(foreground: Color.FromHex("FFB090")))
				.AddChoices("[#F5EFE2]Get a random cat fact[/]", "[#F5EFE2]View saved cat facts[/]", "[#F5EFE2]Exit the app[/]")
			);

			switch (selected) {
				case "[#F5EFE2]Get a random cat fact[/]":
					try {
						CatFactDTO? fact = null;

						await AnsiConsole.Status()
							.StartAsync("Fetching from API...", async ctx => {
								fact = await _service.FetchFact();
							});

						if (fact is null) {
							Console.WriteLine("API returned an empty respsonse.");
							break;
						}

						var jsonString = JsonSerializer.Serialize(fact, new JsonSerializerOptions { WriteIndented = true });
						var jsonText = new JsonText(jsonString)
										.MemberColor(Color.FromHex("FFB090"))
										.StringColor(Color.FromHex("F5EFE2"))
										.NumberColor(Color.FromHex("F2A2A2"))
										.BracesColor(Color.FromHex("FFF1D3"));


						AnsiConsole.MarkupLine("[#F5EFE2]Your random cat fact is:\n[/]");
						AnsiConsole.Write(jsonText);

						await _service.WriteToFile(fact);

						AnsiConsole.MarkupLine("[#F5EFE2]\n\nThe fact has been successfully saved to the txt file.[/]");

					}
					catch (Exception ex) {
						AnsiConsole.MarkupLine($"[#AE2448]\n\n{ex.Message}[/]");
					}


					break;

				case "[#F5EFE2]View saved cat facts[/]":
					string[] lines = { };

					try {
						lines = await _service.ReadFromFile();
					}
					catch (Exception ex) {
						AnsiConsole.MarkupLine($"[#AE2448]\n\n{ex.Message}[/]");
						break;
					}

					if (lines.Length > 50) {
						AnsiConsole.MarkupLine("[#FFB090]The file contains more than 50 facts. Displaying only the first 50...[/]");
					}

					for (int i = 0; i < Math.Min(50, lines.Length); i++) {
						AnsiConsole.MarkupLine($"[#F5EFE2]{Markup.Escape(lines[i])}[/]");
					}
					break;

				case "[#F5EFE2]Exit the app[/]":
					return;
			}

			AnsiConsole.MarkupLine("[#F5EFE2]Press any button to continue...[/]");
			Console.ReadKey();
		}
	}

	private void ClearConsole() {
		AnsiConsole.Clear();
		AnsiConsole.Write(new FigletText("Cat Facts")
		.Centered()
		.Color(Color.FromHex("FFB090")));

		var rule = new Rule("[#F5EFE2]An enterprise-grade B2B feline trivia fetcher[/]");
		rule.Justification = Justify.Center;
		AnsiConsole.Write(rule);

		AnsiConsole.WriteLine();
	}
}
