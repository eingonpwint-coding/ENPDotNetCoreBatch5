# ENPDotNetCoreBatch5
## C# .NET

### Languages and Frameworks

- **C# Language**
- **.NET Framework** 
  - Versions: 1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8 (Windows)
- **.NET Core**
  - Versions: 1, 2, 2.2, 3, 3.1 (VS2019, VS2022 - Windows, Linux, macOS)
- **.NET**
  - Versions: 5 (VS2019), 6 (VS2022), 7, 8 (Windows, Linux, macOS)

### Development Tools

- **Visual Studio Code**
- **Visual Studio 2022**

### Application Types

- **Console App**
- **Windows Forms**
- **ASP.NET Core Web API**
- **ASP.NET Core Web MVC**
- **Blazor WebAssembly**
- **Blazor Server**

## Project Structure

### UI + Business Logic + Data Access => Database

## Example Projects

### Kpay

#### Features:

- **Mobile No** => Transfer
- **Mobile No Check**
- **SLH** => **Collin**
  - **10000 => 0**
  - **-5000 => +5000**
  - **Bank +5000**


MSSQL - Server Name - > select - new query- Select @@servername 
If login password change , security - login - sa (select)- change password
Breakpoint - F9
Line by line - F10
Start with Debugging - F5

SqlClient -> package (System.Data.SqlClient)

DataSet>DataTable>DataRow>DataColumn

### Dapper (crud) need query
- dapper install
- dynamic -> don't have exactly data type** 
- Query(select)
- Execute(create, update,delete)**

### Entity framework (CRUD)
- Microsoft.EntityFrameworkCore(install)**
- Microsoft.EntityFrameworkCore.SQLServer**
- Create DbContext class : DbContext**

### AsNoTracking
-  select * from TBl_Blog with (nolock) 
- commit data (1,2,3,4 (real data),5(not decided yet) <- uncomit data
- insert into
- update tbl_blog
- 1-mgmg 1
- 2-aung aung 2
- 3-ko ko 3-change 3 - ko ko 6 if use no tracking , when try select result old result ,not get realtime data
- 4-aye aye 4
- 5- myo myo 5
- oracle don't have nolock (only pull commit data ) > need to insert commit line
-