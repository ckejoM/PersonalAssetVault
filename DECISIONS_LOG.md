# Architecture Decisions & Tech Debt Log

This document tracks the architectural trade-offs, technical debt, and foundational decisions made during the development of the Personal Asset Vault. 

## 1. Database: SQLite vs. PostgreSQL

[cite_start]**Decision:** We utilized SQLite for the persistence layer instead of PostgreSQL.

**Context & Reasoning:** This application is the foundational piece of a 9-project portfolio demonstrating Clean Architecture. The primary audience (recruiters, peer engineers, and hiring managers) needs to clone and run this repository with zero friction. 
* **Pros:** SQLite runs entirely in-memory or via a local `.db` file. It requires zero Docker containers, connection string configurations, or background services to run. 
* **Cons:** It lacks advanced concurrency, strict relational constraints, and JSON column support found in enterprise engines.

**Future Tech Debt Resolution:**
The `Infrastructure` layer completely abstracts the database via the Repository Pattern and Entity Framework Core. Upgrading to PostgreSQL in the future simply requires swapping the `UseSqlite()` provider to `UseNpgsql()` in the Dependency Injection setup and generating a new migration.

## 2. State Management: Angular Signals vs. NgRx

[cite_start]**Decision:** We implemented Angular Signals for frontend state management rather than NgRx[cite: 37].

**Context & Reasoning:**
* **Overhead vs. Value:** NgRx is the gold standard for massive enterprise applications but introduces significant boilerplate (Actions, Reducers, Selectors, Effects). For a personal CRUD dashboard, this level of indirection is an anti-pattern.
* **Modern Standard:** Angular 16+ introduced Signals, moving away from `Zone.js` for change detection. By using `toSignal` and functional interceptors, we achieved highly optimized, fine-grained reactivity natively.
* **Pros:** Drastically reduced bundle size, simpler mental model (synchronous reads), and eliminated RxJS memory leak risks in component templates.

**Future Tech Debt Resolution:**
If the vault evolves to require complex offline synchronization, optimistic UI updates across multiple collaborative users, or heavily derived state across dozens of views, we will re-evaluate introducing the `@ngrx/signals` (SignalStore) package to maintain modern reactivity while enforcing stricter state transitions.