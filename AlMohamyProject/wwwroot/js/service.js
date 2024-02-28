﻿updateTable();

function updateTable() {
  
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/ServiceApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            data.forEach(rowData => {
                console.log(rowData);
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");
              
               
               
              /*  const column3 = document.createElement("td");*/

                
              
              

               
                column1.innerText = rowData.serviceName;

               
               
                column2.innerHTML = `<a href="/Admin/Service/Form?id=${rowData.serviceId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
               /* column3.innerHTML = `<a href="/Admin/Service/Delete?id=${rowData.serviceId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`;*/

               

                row.appendChild(column1);
                row.appendChild(column2);
              /*  row.appendChild(column3);*/
               
               
               
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}