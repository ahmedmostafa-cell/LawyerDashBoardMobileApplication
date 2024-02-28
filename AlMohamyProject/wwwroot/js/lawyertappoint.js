
updateTableWithoutParameter();
function myFunction2() {
   
    updateTable();
};



function updateTable() {
    const option = document.getElementById("lawyer").value;
    
  
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/LawyerAppointApi/${option}`)
        .then(response => response.json())
        .then(data => {
         
         
            const table = document.getElementById("example");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            let count = 0;
            let sum = 0;
            let delSum = 0;
            data.forEach(rowData => {
               
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");
                const column3 = document.createElement("td");
                const column4 = document.createElement("td");
                const column5 = document.createElement("td");
                const column6 = document.createElement("td");



                column1.innerText = rowData.lawyerName;
                column2.innerText = rowData.weekDay;
                column3.innerText = rowData.fromHour;
                column4.innerText = rowData.morEveFrst;
                column5.innerText = rowData.toHour;
                column6.innerHTML = rowData.morEveScond;

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                tbody.appendChild(row);
            });
         
            document.getElementById("cons").innerText = sum;
            document.getElementById("del").innerText = delSum;
            document.getElementById("count").innerText = count;
            
           
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}




function updateTableWithoutParameter() {
   
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/LawyerAppointApi/`)
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
            


                column1.innerText = rowData.lawyerName;
                column7.innerText = rowData.notes;
                column2.innerText = rowData.weekDay;
                column3.innerText = rowData.fromHour;
                column4.innerText = rowData.morEveFrst;
                column5.innerText = rowData.toHour;
                column6.innerHTML = rowData.morEveScond;
               
                row.appendChild(column1);
                row.appendChild(column7);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                tbody.appendChild(row);
               
            });

           

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}



