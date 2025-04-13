# Licenses-Pro-Center

**License Pro Center (License Processing & Management System)**

License Pro Center is a comprehensive **desktop application** that manages every stage of the **driver licensing process**, including both **local and international licenses**. It handles applications, testing, issuance, renewals, replacements, license holds, and full user management. Designed for organizations that process driver licenses, the system provides a **secure** and **user-friendly** interface to streamline all operations.

---

## 📌 Overview

- Built with **.NET Framework** using **C#**
- UI created with **Windows Forms**
- Backend powered by **SQL Server** via **ADO.NET**
- Supports **CRUD operations**, **user activity tracking**, and **local/international license management**

---

## 🚦 Key Features

### Local License Management

- 📝 **Apply for Local Driving License**
  - Link to person profile
  - Choose from multiple license classes:
    - Light, Heavy, Commercial, Bus, Agricultural
  - Schedule and complete:
    - 👁️ Vision Test  
    - 🧠 Written Test  
    - 🚗 Driving Test  
  - 💳 Pay application and retake fees  
  - ✅ Manually issue license after passing all tests  
  - 📄 View full application history
- 🔁 **Renew License**
- ❗ **Replace Lost/Damaged Licenses**
- ❌ **Cancel/Delete Applications**
- ⏸️ **Hold & Release Licenses**

### International License Management

- 🌍 **Apply for International License**
  - Requires an existing local license
  - Full application lifecycle management

---

## 🗂️ Application & Entity Management

- 👤 **People**: Full CRUD operations for applicants
- 👥 **Users**: Manage employee/system users (admin & staff roles)
- 🚘 **Drivers**: Track info for issued licenses
- 🗂️ **Applications**: View, update, and manage license applications
- 🧾 **Application Types**: Define categories and fee structures
- 🪪 **License Classes**: Set up types with rules (age, fee, validity)

---

## 🧪 Testing & Appointments

- 🗓️ **Test Scheduling**: Book vision, written, and driving tests
- 🧾 **Test Results**: Record pass/fail with comments
- 🔍 **Test Types**: Manage all testing categories

---

## 👤 Account Management

- 🙍‍♂️ View user profile information
- 🔐 Change user password
- 🧾 **Activity Tracking**: Log all user actions for traceability

---

## 💻 Technologies Used

| Category              | Technology              |
|-----------------------|--------------------------|
| Programming Language  | C#                      |
| Framework             | .NET Framework          |
| UI                    | Windows Forms           |
| Database              | Microsoft SQL Server     |
| Data Access           | ADO.NET                 |
| Architecture          | 3-tier                  |
| Deployment            | Standalone Desktop App  |

---

## 🗃️ Database Setup

To restore the database:

1. 📥 Download `DVLD.bak` from the `Database/` folder  
2. 📂 Place it in a preferred location (e.g., `C:\`)  
3. 🛠 Open **SQL Server Management Studio (SSMS)**  
4. 📑 Right-click on **Databases → Restore Database**  
5. 📌 Select **Device → Browse → Choose `DVLD.bak`**  
6. ✅ Click **OK** to complete the restore  

---

## 🎬 Demo

A complete video walkthrough with voice explanation is coming soon.  
🎥 **Link will be added here once available**

---

## 📈 Future Plans

- 🚀 **Version 2**: Refactored codebase with improved maintainability
- 🌐 **Web Version**: Online version for broader accessibility

---

