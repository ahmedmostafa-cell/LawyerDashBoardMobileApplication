
function myFunction() {
    
    updateTable();
}

function updateTable() {
    const option = document.getElementById("lawyer").value;
  
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/LawyerCostConsultApi/${option}`)
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
                const column3 = document.createElement("td");
               
                const column5 = document.createElement("td");
                const column6 = document.createElement("td");
             


                column1.innerText = rowData.lawyerName;
                column2.innerText = rowData.consultingPeriod;
                column3.innerText = rowData.consultingPeriodCost;
              
                column5.innerHTML = `<a href="/Admin/LawyerPeriodCostConsult/Form?id=${rowData.lawyerPeriodCostConsultId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
              
                column6.innerHTML = `<a href="/Admin/LawyerPeriodCostConsult/Delete?id=${rowData.lawyerPeriodCostConsultId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`
                //column7.innerText = rowData.requestType;
                //column8.innerText = rowData.userFirstName;

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
              
                row.appendChild(column5);
                row.appendChild(column6);
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}