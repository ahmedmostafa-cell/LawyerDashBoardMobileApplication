
updateTable();

function updateTable() {
  
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/PaymentGateApi/`)
        .then(response => response.json())
        .then(data => {
          
            console.log(data);
            const table = document.getElementById("example");
            const tbody = table.getElementsByTagName("tbody")[0];
          
            console.log(data);
            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            data.forEach(rowData => {
                console.log(rowData);
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");
              
               
               
                const column3 = document.createElement("td");
                const column4 = document.createElement("td");
                const column5 = document.createElement("td");
              

                column2.className ="avatar avatar-xl";
                column1.innerText = rowData.paymentGateTitle;
                column2.innerHTML = `<img src="/Uploads/${rowData.paymentGateImage}" alt="Avatar" class="rounded-circle">`;
                column3.innerText = rowData.activationStatus;
                column4.innerHTML = `<a href="/Admin/PaymentGate/Form?id=${rowData.paymentGatesId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
                column5.innerHTML = `<a href="/Admin/PaymentGate/Delete?id=${rowData.paymentGatesId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`;

               

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
               
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}