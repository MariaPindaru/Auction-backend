# Auction Platform Application

This project implements the core service logic and business rules for an auction platform, handling auction creation, bidding, and user validation workflows. The solution enforces complex validation rules to ensure fair auction practices and reliable user interactions, tested through an extensive suite of unit tests.

## Built With

- **.NET Core**: Developed using .NET Core for cross-platform support, scalability, and high-performance service execution.
- **SQL Database**: The primary database is SQL Server, but the architecture is designed to support other SQL databases like MySQL, Oracle, or PostgreSQL if adapted.

## Project Structure

- **Domain Layer**: Defines core business entities (e.g., Auction, Bid, User) and rules for data validation. This layer implements critical validation logic, such as enforcing auction timing, currency consistency, and duplicate checks on product descriptions.
  
- **Service Layer**: Manages the business logic for auction operations. It provides functionalities for:
  - Auction creation and termination, validating that start dates are not in the past and that each auction has a unique transaction currency.
  - Bidding processes, ensuring each bid increment is within predefined limits and higher than the previous bid.
  - Enforcing user restrictions based on reliability scores and managing configurable auction limits per user.
  - Detecting duplicate product listings based on description similarity.

- **Data Layer**: Implements data access to the SQL database using the Data Mapper pattern. It handles CRUD operations on entities, including auctions, bids, users, and configuration settings. The Data Layer separates database logic from business rules, enabling flexible persistence and easier maintenance. The configuration settings are stored here as well, allowing adjustments without recompilation.

- **Configuration Settings**: Critical settings (e.g., maximum active auctions per user, reliability thresholds, penalty duration) are managed in a configuration file or database, allowing adjustments without recompiling the application.

- **Unit Testing**: Over 200 unit tests are written using NUnit. These tests validate:
  - Business rule adherence, including property validations and state transitions.
  - Error handling, ensuring correct exceptions or results when invalid data is input.
  - Code coverage of over 90% for Domain and Service Layers, with mocking for isolated testing.

## Key Features

1. **Auction Lifecycle Management**
   - Users can initiate auctions, with validations on start and end dates.
   - Auctions can be ended either by reaching the end date or by the user who created them, with data becoming immutable upon completion.
  
2. **Bidding Logic**
   - Each auction has a starting price and a specified currency; subsequent bids must increment the price in this currency, respecting maximum increment rules.
   - All bids are tracked, and only valid, incremented bids are accepted.

3. **User Reliability System**
   - A dynamic scoring system reflects user reliability. The score starts at a configurable value, and subsequent scores are averaged over the last three months.
   - If a user’s score falls below a threshold, they are restricted from starting new auctions for a specified period.

4. **Duplicate Detection**
   - The application checks for duplicate products using Levenshtein distance, allowing only unique listings by the same user. Listings by different users are allowed even if they are similar.

5. **Auction Limits**
   - Configurable limit on the number of active auctions per user, set via configuration.
