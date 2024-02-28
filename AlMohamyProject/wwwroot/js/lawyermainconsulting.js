updatetablewithoutparameters();
function myFunction() {
   
    updateTable();
}

function updateTable() {
    const option = document.getElementById("lawyer").value;
  
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/LawyerMainConsultingApi/${option}`)
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
                const column8 = document.createElement("td");
                const column9 = document.createElement("td");

                column1.innerText = rowData.lawyerUserName;
                column2.innerText = rowData.mainConsultingTitle;
                column3.innerText = rowData.consulting30MinutesCost;
                column4.innerHTML = rowData.consulting60MinutesCost
                column5.innerText = rowData.consulting90MinutesCost;
                column6.innerHTML = `<a href="/Admin/LawyersMainConsultings/Form?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
                column7.innerHTML = `<a href="/Admin/LawyersMainConsultings/Delete?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`;
                column8.innerText = rowData.status;
                column9.innerHTML = `<a href="/Admin/LawyersMainConsultings/Approve?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                row.appendChild(column7);
                row.appendChild(column8);
                row.appendChild(column9);
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}



function updatetablewithoutparameters() {
   

    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/LawyerMainConsultingApi/`)
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
                const column8 = document.createElement("td");
                const column9 = document.createElement("td");

                column1.innerText = rowData.lawyerUserName;
                column2.innerText = rowData.mainConsultingTitle;
                column3.innerText = rowData.consulting30MinutesCost;
                column4.innerHTML = rowData.consulting60MinutesCost
                column5.innerText = rowData.consulting90MinutesCost;
                column6.innerHTML = `<a href="/Admin/LawyersMainConsultings/Form?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> تعديل</a >`;
                column7.innerHTML = `<a href="/Admin/LawyersMainConsultings/Delete?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> حذف</a >`;
                column8.innerText = rowData.status;
                if (rowData.status === "مفعل") {
                    column9.innerHTML = `<a href="/Admin/LawyersMainConsultings/Approve?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> تعليق</a >`;
                }
                else
                {
                    column9.innerHTML = `<a href="/Admin/LawyersMainConsultings/Approve?id=${rowData.lawyersMainConsultingsId}"  class='btn btn-info text-white' style='cursor:pointer'> تفعيل</a >`;
                }
                
                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                row.appendChild(column7);
                row.appendChild(column8);
                row.appendChild(column9);
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}