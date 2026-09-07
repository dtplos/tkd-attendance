# TKD Attendance

Branch-scoped attendance, membership, and renewal-reminder system for a taekwondo studio with multiple branches.

## Stack
- ASP.NET Core (Blazor Server), .NET 10
- Entity Framework Core + SQLite
- ASP.NET Core Identity (cookie auth, role + branch-scoped)
- Hangfire (in-memory) for the daily renewal-reminder job

## Features
- Branch-scoped login: SuperAdmin (all branches) / BranchAdmin (their branch only)
- Student records: name, contact, age, ID number, belt rank, regular class
- Per-branch class schedule (day/time) linked to attendance check-in
- Membership & payment tracking (monthly or class-pack), with renewal reminders sent to branch admins 3 days before expiry (daily job, 8am Singapore time)
- Daily report (signups + attendance by class), printable / exportable to PDF via the browser
- Attendance history — filter by day, month, year, or a custom date range
- Search and filter students by name, belt, or class
- SuperAdmin can create, reset the password for, and remove branch admins

## Running locally

### Prerequisites
- .NET 10 SDK
- `dotnet-ef` global tool: `dotnet tool install --global dotnet-ef`

### Setup
```bash
git clone <repo-url>
cd TkdAttendance
dotnet restore
dotnet dev-certs https --trust
dotnet ef database update
dotnet run
```
Open the printed localhost URL and log in with:
- Email: `admin@tkd.local`
- Password: `ChangeMe123!` — change this before using the app for anything real; it's a public seed value sitting in the source code.

### Database
SQLite, single file (`tkd.db`), created locally the first time migrations run. Not committed to git (see `.gitignore`) — each machine builds its own copy, and there's no automatic sync of data between machines.

## Project structure
Components/
Account/ - Login page
Layout/ - MainLayout, NavMenu
Pages/ - all app pages (Students, Attendance, Classes, Reports, Admins, etc.)
Data/ - AppDbContext, seed data, current-user service
Jobs/ - RenewalReminderJob (Hangfire)
Models/ - EF Core entities
Services/ - email sender


## Updating the repo
```bash
git add .
git commit -m "Describe your change"
git push
```

## Known limitations / next steps
- Reminder emails currently just log to the console (`ConsoleEmailSender`) — swap in SendGrid/Mailgun before relying on this for real notifications
- Not deployed anywhere yet — runs locally / on-LAN only
- `IdNumber` field on students may hold PDPA-regulated data depending on what's entered — worth a policy decision before this goes further

## Deploying
Not set up yet. Candidates for a small SQLite-backed Blazor Server app: Railway, Fly.io.