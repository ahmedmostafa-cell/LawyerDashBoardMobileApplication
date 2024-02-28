
updateTableWithoutParameter();
function myFunction() {

    updateTable();
};


function myFunctionDate1() {

    updateTableDate1();
};




function myFunctionDate2() {

    updateTableDate1();
};

function updateTable() {
    const option = document.getElementById("lawyer").value;
    const optionDate1 = document.getElementById("exampleInputPassword1").value;
    const optionDate2 = document.getElementById("exampleInputPassword2").value;
    if (optionDate1 === "" && optionDate2 === "")
    {
        // Make an API call to fetch data based on the selected option
        fetch(`https://habibaahmedm-002-site10.atempurl.com/api/ConsultingEstablishApi/${option}`)
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
                    const column7 = document.createElement("td");
                    const column8 = document.createElement("td");


                    column1.innerText = rowData.consultingDateAndTime;
                    column2.innerText = rowData.consultingType;
                    column3.innerText = rowData.createdDate;
                    column4.innerText = rowData.createdBy;
                    column5.innerText = rowData.theConsultingPaidValue;

                    column6.innerHTML = `<a href="/Admin/ConsultingEstablish/Form?id=${rowData.consultingId}"  class='btn btn-info text-white' style='cursor:pointer'> تفاصيل</a >`
                    column7.innerText = rowData.requestType;
                    column8.innerText = rowData.userFirstName;

                    row.appendChild(column1);
                    row.appendChild(column2);
                    row.appendChild(column3);
                    row.appendChild(column4);
                    row.appendChild(column5);
                    row.appendChild(column6);
                    tbody.appendChild(row);
                    if (rowData.theConsultingPaidValue != null)
                    {
                        sum = sum + parseInt(rowData.theConsultingPaidValue);
                    }
                    if (rowData.createdBy != null) {
                        delSum = delSum + parseInt(rowData.createdBy);
                    }
                  
                   
                    count++;
                    document.getElementById("count").innerText = data.length;
                });

                document.getElementById("cons").innerText = sum;
                document.getElementById("del").innerText = delSum;
                document.getElementById("count").innerText = count;


            })
            .catch(error => {
                console.error("Error fetching data:", error);
            });
    }
    else
    {
        updateTableDate1();
    }
  
   
}




function updateTableWithoutParameter() {
   
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/ConsultingEstablishApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            let count = 0;
            let sum = 0;
            let delSum = 0;
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


                column1.innerText = rowData.consultingDateAndTime;
                column2.innerText = rowData.consultingType;
                column3.innerText = rowData.createdDate;
                column4.innerText = rowData.createdBy;
                column5.innerText = rowData.theConsultingPaidValue;

                column6.innerHTML = `<a href="/Admin/ConsultingEstablish/Form?id=${rowData.consultingId}"  class='btn btn-info text-white' style='cursor:pointer'> تفاصيل</a >`
                column7.innerText = rowData.requestType;
                column8.innerText = rowData.userFirstName;

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                tbody.appendChild(row);
                if (rowData.theConsultingPaidValue != null) {
                    sum = sum + parseInt(rowData.theConsultingPaidValue);
                }
                if (rowData.createdBy != null) {
                    delSum = delSum + parseInt(rowData.createdBy);
                }
                count++;
            });

            document.getElementById("cons").innerText = sum;
            document.getElementById("del").innerText = delSum;
            document.getElementById("count").innerText = count;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}



function updateTableDate1() {
    const option = document.getElementById("lawyer").value;
    const optionDate1 = document.getElementById("exampleInputPassword1").value;
    const optionDate2 = document.getElementById("exampleInputPassword2").value;
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/ConsultingEstablishApi/${option}/${optionDate1}/${optionDate2}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            let sum = 0;
            let delSum = 0;
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
                const column8 = document.createElement("td");


                column1.innerText = rowData.consultingDateAndTime;
                column2.innerText = rowData.consultingType;
                column3.innerText = rowData.createdDate;
                column4.innerText = rowData.createdBy;
                column5.innerText = rowData.theConsultingPaidValue;

                column6.innerHTML = `<a href="/Admin/ConsultingEstablish/Form?id=${rowData.consultingId}"  class='btn btn-info text-white' style='cursor:pointer'> تفاصيل</a >`
                column7.innerText = rowData.requestType;
                column8.innerText = rowData.userFirstName;

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                tbody.appendChild(row);
                if (rowData.theConsultingPaidValue != null) {
                    sum = sum + parseInt(rowData.theConsultingPaidValue);
                }
                if (rowData.createdBy != null) {
                    delSum = delSum + parseInt(rowData.createdBy);
                }
                count++;
            });

            document.getElementById("cons").innerText = sum;
            document.getElementById("del").innerText = delSum;
            document.getElementById("count").innerText = count;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}