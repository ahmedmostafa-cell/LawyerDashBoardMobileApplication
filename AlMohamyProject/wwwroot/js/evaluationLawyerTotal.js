﻿updateTable();

function updateTable() {
  
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/CustomerEvaluationApi/`)
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
                const column4 = document.createElement("td");
                const column5 = document.createElement("td");
                const column6 = document.createElement("td");
                const column7 = document.createElement("td");
                //const column8 = document.createElement("td");
                //const column9 = document.createElement("td");
              

                column2.className ="avatar avatar-xl";
                column1.innerText = rowData.createdBy;
                column2.innerHTML = `<img src="/Uploads/${rowData.evaluaterImage}" alt="Avatar" class="rounded-circle">`;
                column3.innerText = rowData.updatedBy;
                column4.className = "avatar avatar-xl";
                column4.innerHTML = `<img src="/Uploads/${rowData.toBeEvaluatedImage}" alt="Avatar" class="rounded-circle">`;
                column5.innerText = rowData.evaluationText;
                column6.innerText = rowData.startsNo;
                column7.innerText = rowData.notes;
                //column8.innerHTML = `<a href="/Admin/PaymentGate/Form?id=${rowData.evaluationId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
                //column9.innerHTML = `<a href="/Admin/PaymentGate/Delete?id=${rowData.evaluationId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`;

               

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                row.appendChild(column7);
                //row.appendChild(column8);
                //row.appendChild(column9);
               
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
};


function myFunctionn()
{
   
    updateTable1();
}



function updateTable1() {
  
    const option = document.getElementById("lawyer").value;
   
    // Make an API call to fetch data based on the selected option //https://habibaahmedm-002-site10.atempurl.com/
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/CustomerEvaluationApi/Lawyer/${option}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            let sum = 0;
            let count = 0;
            data.forEach(rowData => {
                console.log(rowData);
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");
                const column3 = document.createElement("td");
                const column4 = document.createElement("td");
                const column5 = document.createElement("td");
                const column6 = document.createElement("td");
                const column7 = document.createElement("td");
                //const column8 = document.createElement("td");
                //const column9 = document.createElement("td");


                column2.className = "avatar avatar-xl";
                column1.innerText = rowData.createdBy;
                column2.innerHTML = `<img src="/Uploads/${rowData.evaluaterImage}" alt="Avatar" class="rounded-circle">`;
                column3.innerText = rowData.updatedBy;
                column4.className = "avatar avatar-xl";
                column4.innerHTML = `<img src="/Uploads/${rowData.toBeEvaluatedImage}" alt="Avatar" class="rounded-circle">`;
                column5.innerText = rowData.evaluationText;
                column6.innerText = rowData.startsNo;
                column7.innerText = rowData.notes;
                //column8.innerHTML = `<a href="/Admin/PaymentGate/Form?id=${rowData.evaluationId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
                //column9.innerHTML = `<a href="/Admin/PaymentGate/Delete?id=${rowData.evaluationId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`;



                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                row.appendChild(column7);
                //row.appendChild(column8);
                //row.appendChild(column9);

                tbody.appendChild(row);
                sum = sum + parseInt(rowData.startsNo);
                count++;
            });
           
            document.getElementById("LawyerTotalEvaluation").innerText = sum / count;
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}




