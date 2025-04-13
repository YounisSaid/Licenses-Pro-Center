# Licenses-Pro-Center
License Pro Center (License Processing & Management System)
License Pro Center is a comprehensive desktop application designed to manage every stage of the driver licensing process—covering local and international licenses. It handles applications, testing, issuance, renewals, replacements, license holds, and full system user management. The system is ideal for organizations handling driver license processing and aims to streamline and track every operation through a secure and user-friendly interface.
________________________________________
Overview
License Pro Center supports a complete flow for issuing and managing driver licenses.
Built using the .NET Framework, it uses C# and Windows Forms for the interface, with SQL Server as the database backend connected via ADO.NET.
The system features full CRUD operations across all entities, accountability through user activity tracking, and support for both local and international license operations.
________________________________________
Key Features 
Local License Management
•	📝 Apply for Local Driving License
o	Link to person profile
o	Choose from multiple license classes (e.g., light, heavy, commercial, bus, agricultural)
o	Schedule and complete:
	👁️ Vision Test
	🧠 Written Test
	🚗 Driving Test
o	💳 Pay application and retake fees
o	✅ Issue license manually upon passing all tests
o	📄 View application history
•	🔁 Renew License
•	❗ Replace Lost or Damaged Licenses
•	❌ Cancel or Delete Applications
•	⏸️ Hold and Release Licenses
________________________________________
International License Management
•	🌍 Apply for International License
o	Requires existing local license
o	Full application management
________________________________________
Application and Entity Management
•	👤 People: Manage individual applicants with full CRUD support
•	👥 Users: Handle employee/system user accounts (admin or staff)
•	🚘 Drivers: Track driver info linked to issued licenses
•	🗂️ Applications: View, update, and manage license applications
•	🧾 Application Types: Define and manage license categories and associated fees
•	🪪 License Classes: Configure driving license types with age, fee, and validity rules
________________________________________
Testing & Appointments
•	🗓️ Test Scheduling: Book appointments for all test types
•	🧾 Test Results: Record pass/fail results with notes
•	🔍 Test Types: Manage categories (Vision, Written, Driving)
________________________________________
Account Management
•	🙍‍♂️ User Info View
•	🔐 Change Password
•	🧾 Operation Tracking: All activities linked to the logged-in user for traceability
________________________________________
Technologies Used
•	💻 Programming Language: C#
•	🧱 Framework: .NET Framework
•	🖼️ User Interface: Windows Forms
•	🗄️ Database: Microsoft SQL Server
•	🔗 Data Access: ADO.NET
•	🏗️ Architecture: 3-tier architecture
•	🖥️ Deployment: Standalone desktop application
________________________________________
🗃 Database Setup
Restore from Backup (.bak):
📥 Download DVLD.bak from the Database/ folder.
📂 Place it in a preferred location (e.g., C:\).
🛠 Open SQL Server Management Studio (SSMS).
📑 Right-click on Databases → Restore Database.
📌 Select Device → Browse → Choose DVLD.bak.
✅ Click OK to restore.
________________________________________
Demo
A full video walkthrough of the system (with voice explanation) will be available soon.
🎥 Link coming soon.
________________________________________
Future Plans
•	🚀 Release Version 2 with cleaner code structure and maintainability enhancements
•	🌐 Develop a web version of License Pro Center for broader accessibility
________________________________________

