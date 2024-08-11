# Apollo Medical Consultation Application

## Introduction

Apollo is an innovative web application designed to provide immediate, accessible, and cost-effective medical consultations and advice. It leverages the power of a Large Language Model (LLM), specifically Google Gemini AI, to deliver accurate medical information and guidance. The application is particularly beneficial for individuals without health insurance or sufficient funds, offering a reliable alternative to unnecessary ER visits by providing timely medical support.

Apollo's core objective is to bridge the gap in healthcare access by offering a platform where users can describe their symptoms, receive a preliminary diagnosis, and, if necessary, request a second opinion from a global network of doctors. 

## Technology Stack

- **Google Gemini AI**: The application utilizes Google Gemini AI for processing medical queries and providing responses. An unofficial C# SDK is used to interface with both the basic and vision models. More information can be found on the [Google Generative AI GitHub Repository](https://github.com/gunpal5/Google_GenerativeAI?tab=readme-ov-file#usage).
- **ASP.NET Core Razor Pages MVC**: The application is built using ASP.NET Core Razor Pages, providing a clean and maintainable structure.
- **PostgreSQL**: The backend database is powered by PostgreSQL, offering robust and scalable data management.
- **Docker**: Docker and Docker Compose are used to streamline the development, testing, and deployment process.

## Features

- **Symptom Checker**: Users can describe their symptoms and receive an immediate, AI-generated diagnosis, including advice on whether an ER visit is necessary.
- **Image Upload**: Users can upload images related to their symptoms, which are analyzed by the vision model of the LLM to enhance the diagnostic process.
- **Medical Knowledge Base**: A comprehensive, AI-powered encyclopedia where users can query medical information on various conditions, treatments, and more.
- **Second Opinion Requests**: Users can request a second opinion from a bank of doctors worldwide, with the option to select doctors based on their expertise and ratings.
- **Consultation History**: Users have access to a history of their consultations, including symptoms, diagnoses, and any second opinions received.
- **Security and Compliance**: The application adheres to HIPAA regulations, ensuring that all user data is encrypted and securely stored.
- **Responsive UI**: The application features a modern, responsive design, providing an optimal user experience across devices.

## Setting Up the Local Environment

### Prerequisites

- Docker
- Docker Compose

### Step 1: Clone the Repository

```bash
git clone https://github.com/yourusername/apollo.git
cd apollo
```

### Step 2: Set Up Environment Variables

Copy the `.env.example` file to `.env` and fill in the necessary environment variables:

```bash
cp .env.example .env
```

Edit the `.env` file to include your Google Gemini API key and PostgreSQL database credentials:

```env
GoogleGemini__ApiKey=your_google_gemini_api_key
Database_DB=your_database_name
Database_User=your_database_user
Database_Password=your_database_password
ConnectionStrings__DefaultConnection=your_connection_string
```

### Step 3: Run the Application Using Docker Compose

Use Docker Compose to build and run the application:

```bash
docker-compose up --build
```

This command will:

1. Build the application Docker image.
2. Set up and run a PostgreSQL container for the database.
3. Run the Apollo application in a container with the necessary environment variables.

### Step 4: Access the Application

Once the application is running, you can access it via your web browser:

- **Application**: `http://localhost:8080`

### Step 5: Stopping the Application

To stop the application and remove the containers, run:

```bash
docker-compose down
```

## Application Structure

The application is organized as follows:

- **Apollo**: The main application codebase.
- **Apollo/Controllers**: API controllers for handling user requests.
- **Apollo/Models**: Data models representing the database schema.
- **Apollo/Pages**: Razor Pages for the web UI.
- **Apollo/Services**: Services for integrating with external APIs, including Google Gemini AI.
- **Apollo/Shared**: Shared views and layout files.
- **Dockerfile**: The Dockerfile used to build the application container.
- **docker-compose.yml**: The Docker Compose configuration file.