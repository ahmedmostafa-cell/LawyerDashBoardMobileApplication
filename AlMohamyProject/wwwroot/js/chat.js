

function customerChoose() {
    
    updateTableDate1();
};


function lawyerChoose() {

    updateTableDate1();
};
















function updateTableDate1() {
   
    const customerchoose = document.getElementById("customer").value;
    const lawyerchoose = document.getElementById("lawyer").value;
   
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/ChatHistoryApi/${customerchoose}/${lawyerchoose}`)
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


               
                   
               
                column1.innerText = rowData.senderFirstName;
                column2.innerHTML = `<a href="/Uploads/${rowData.senderDocument}" target="_blank"  class="btn btn-primary">ملف مرفق</a>`;
                column3.innerHTML = `<audio controls autobuffer="autobuffer" title="Sound"><source src='/Uploads/${rowData.senderAudio}'/></audio>`;
                column4.innerText = rowData.senderUserType;
                column5.innerText = rowData.recieverFirstName;
                column6.innerText = rowData.senderText;
                column7.innerText = rowData.createdDate;



                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);
                row.appendChild(column4);
                row.appendChild(column5);
                row.appendChild(column6);
                row.appendChild(column7);


                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}