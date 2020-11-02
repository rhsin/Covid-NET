# COVID-NET
<table>
<tr>
<td>
  A web app for querying & saving COVID data to authenticated user accounts (Cases/Deaths data-set from 6 million U.S. records). This project is for learning purposes to try various .NET/React libraries, as well as Identity Server for authentitcation. Testing is handled in the ClientApp directory (React) and separate XUnit testing project @ https://github.com/rhsin/Covid-NET-Tests.
</td>
</tr>
</table>


## Site

### DailyCount
Users can use the search form to query specific data by county, state, & month. With the option to save entries to their account list.

![](/Dailycount.png?raw=true)


Users can also view, add, & remove entries from their list, and see other user lists through the select forms.

![](/Countlist.png?raw=true)


Historical case data can be isolated by month for further analysis such as monthly averages or trends.

![](/Dailycountdata.png?raw=true)


## Mobile support
This app uses a responsive grid to cater to different devices & sizes. 


## Built with 
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) v3.1.9
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019) v15.0.0
- [React/Context](https://reactjs.org/) v17.0.1
- [Reactstrap](https://reactstrap.github.io/) v8.1.0


## Testing - https://github.com/rhsin/Covid-NET-Tests
- [XUnit](https://xunit.net/) v2.4.0
- [React Testing Library](https://xunit.net/) v11.1.0


## Contact
Created by Ryan Hsin - please feel free to contact me!
