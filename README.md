# WorldCup App

A desktop application built with .NET (WPF) that provides real-time (or near real-time) information about World Cup matches, including schedules, results, and team details. This project serves as an example of building a modern .NET desktop application using the **Model-View-ViewModel (MVVM)** architectural pattern.

---

## Table of Contents

* [Features](#features)
* [Technologies Used](#technologies-used)
* [Architecture](#architecture)
* [Getting Started](#getting-started)
    * [Prerequisites](#prerequisites)
    * [Installation](#installation)
    * [Running the Application](#running-the-application)
* [Project Structure](#project-structure)
* [How to Contribute](#how-to-contribute)
* [License](#license)
* [Contact](#contact)

---

## Features

* **World Cup Match Schedule:** View upcoming and past match fixtures.
* **Match Results:** See the scores and details of completed matches.
* **Team Information:** Browse details about participating teams.
* **Data from External API:** Fetches data from a FIFA World Cup API (or similar source) to ensure up-to-date information.
* **Localized Content (Planned/Partial):** Designed with localization in mind for potential multi-language support.

---

## Technologies Used

* **.NET 8 (or later):** The core framework for the application.
* **WPF (Windows Presentation Foundation):** For building the rich desktop user interface.
* **MVVM Light Toolkit (or similar MVVM framework):** To facilitate the MVVM pattern (if explicitly used).
* **HTTPClient:** For making API requests.
* **JSON.NET (Newtonsoft.Json):** For deserializing API responses.
* **C#:** The primary programming language.

---

## Architecture

This application strictly follows the **Model-View-ViewModel (MVVM)** architectural pattern to ensure a clear separation of concerns, improve testability, and enhance maintainability.

* **Model:** Represents the data and business logic (e.g., `Match`, `Team` objects, `MatchRepository`).
* **View:** The UI elements defined in XAML (`MainWindow.xaml`, `UserControls`). These are responsible for displaying data and handling user interaction.
* **ViewModel:** Acts as an intermediary between the View and the Model. It exposes data from the Model to the View and handles commands from the View. It does not have direct knowledge of the View.

---

## Getting Started

Follow these steps to get the WorldCup app up and running on your local machine.

### Prerequisites

* **Visual Studio 2022 (or later):** With the ".NET desktop development" workload installed.
* **.NET SDK 8.0 (or later):** Ensure you have the correct .NET SDK installed.

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://[https://github.com/dcaric/WorldCup.git](https://github.com/dcaric/WorldCup.git)
    cd WorldCup
    ```
2.  **Open in Visual Studio:**
    Open the `WorldCup.sln` file in Visual Studio.

### Running the Application

1.  **Restore NuGet Packages:** Visual Studio should automatically restore the necessary NuGet packages. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."
2.  **Build the Solution:** Press `Ctrl+Shift+B` or go to `Build > Build Solution`.
3.  **Run the Application:** Press `F5` or click the "Start" button in Visual Studio.

---

## Project Structure

The project is organized into logical folders to maintain clarity and separation of concerns:

* `WorldCup/`: The main application project.
    * `Domain/`: Contains the Model classes (e.g., `Match`, `Team`).
    * `Repositories/`: Handles data access logic, potentially interacting with an external API (e.g., `MatchRepository`, `FifaWorldCupAPI`).
    * `ViewModels/`: Contains the ViewModel classes that expose data to the Views and handle UI logic.
    * `Views/`: Contains the main window and any additional views.
    * `UserControls/`: Reusable UI components.
    * `Properties/`: Application settings, resources (for localization), etc.

---

## How to Contribute

Contributions are welcome! If you have suggestions for improvements, new features, or bug fixes, please feel free to:

1.  Fork the repository.
2.  Create a new branch (`git checkout -b feature/YourFeature`).
3.  Make your changes.
4.  Commit your changes (`git commit -m 'Add some feature'`).
5.  Push to the branch (`git push origin feature/YourFeature`).
6.  Open a Pull Request.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Contact

If you have any questions or feedback, feel free to reach out:

* **Your GitHub Profile:** [https://github.com/dcaric](https://github.com/dcaric)
* **Your Website:** [www.dcapps.net](http://www.dcapps.net)
