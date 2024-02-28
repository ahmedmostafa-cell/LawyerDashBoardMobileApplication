

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
  
    let valueAddedTax;
    let appProfitPercent;
    const option = document.getElementById("lawyer").value;
    const optionDate1 = document.getElementById("exampleInputPassword1").value;
    const optionDate2 = document.getElementById("exampleInputPassword2").value;
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SettingApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            valueAddedTax = parseInt(data.valueAddedTax);
            appProfitPercent = parseInt(data.appProfitPercent);
            if (optionDate1 === "" && optionDate2 === "")
            {
                fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SalesPerLawyerApi/${option}`)
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
                        let docSum = 0;
                        let serviceincome = 0;
                        let appprofitmargin = 0;
                        let serviceincomeminusapprofit = 0;
                        let vatofappprofit = 0;
                        let appprofitminusvat = 0;
                        let applypaydeduct = 0;
                        let madadeduct = 0;
                        let mastervisadeduct = 0;
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



                            column1.innerText = rowData.consultingDateAndTime;
                            column2.innerText = rowData.consultingType;
                            column3.innerText = rowData.createdDate;
                            column4.innerText = rowData.createdBy; // المبلغ المدفوع للتفويض
                            column5.innerText = rowData.theConsultingPaidValue;
                            column6.innerText = rowData.theDocumentationPaidValue;
                            column7.innerHTML = `<a href="/Admin/ConsultingEstablish/Form?id=${rowData.consultingId}"  class='btn btn-info text-white' style='cursor:pointer'> تفاصيل</a >`


                            row.appendChild(column1);
                            row.appendChild(column2);
                            row.appendChild(column3);
                            row.appendChild(column4);
                            row.appendChild(column5);
                            row.appendChild(column6);
                            row.appendChild(column7);
                            tbody.appendChild(row);
                            if (rowData.theConsultingPaidValue !== null) {
                                sum = sum + parseInt(rowData.theConsultingPaidValue);
                                if (rowData.paymentGateTitle === "بطاقات مدي") {

                                    madadeduct = (madadeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));

                                }
                                else if (rowData.paymentGateTitle === "بطاقات فيزا و ماستر كارد") {
                                    mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                                }
                                else if (rowData.paymentGateTitle === "apple pay") {
                                    applypaydeduct = (applypaydeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                                }
                            }
                            if (rowData.createdBy !== null) {
                                delSum = delSum + parseInt(rowData.createdBy);
                                if (rowData.paymentGateTitleDelegation === "بطاقات مدي") {

                                    madadeduct = (madadeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));

                                }
                                else if (rowData.paymentGateTitleDelegation === "بطاقات فيزا و ماستر كارد") {

                                    mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));

                                }
                                else if (rowData.paymentGateTitleDelegation === "apple pay") {

                                    applypaydeduct = (applypaydeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));
                                }

                            }
                            if (rowData.theDocumentationPaidValue !== null) {
                                docSum = docSum + parseInt(rowData.theDocumentationPaidValue);
                                if (rowData.paymentGateTitle === "بطاقات مدي") {

                                    madadeduct = (madadeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));

                                }
                                else if (rowData.paymentGateTitle === "بطاقات فيزا و ماستر كارد") {
                                    mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));
                                }
                                else if (rowData.paymentGateTitle === "apple pay") {
                                    applypaydeduct = (applypaydeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));
                                }
                            }

                            count++;
                        });

                        serviceincome = sum + delSum + docSum;
                        appprofitmargin = (appProfitPercent * (serviceincome / 100)).toFixed(2);

                        serviceincomeminusapprofit = (serviceincome - appprofitmargin).toFixed(2);
                        vatofappprofit = ((appprofitmargin * valueAddedTax) / 100).toFixed(2);
                        appprofitminusvat = (appprofitmargin - vatofappprofit).toFixed(2);
                        document.getElementById("serviceincome").innerText = serviceincome;
                        document.getElementById("serviceno").innerText = count;
                        document.getElementById("appprofitmargin").innerText = appprofitmargin;
                        document.getElementById("serviceincomeminusapprofit").innerText = serviceincomeminusapprofit;
                        document.getElementById("vatofappprofit").innerText = vatofappprofit;
                        document.getElementById("appprofitminusvat").innerText = appprofitminusvat;
                        document.getElementById("madapercent").innerText = madadeduct.toFixed(2);
                        document.getElementById("mastervisapercent").innerText = mastervisadeduct.toFixed(2);
                        document.getElementById("applypaypercent").innerText = applypaydeduct.toFixed(2);
                        document.getElementById("appprofitminusegatespercent").innerText = (appprofitminusvat - (madadeduct + mastervisadeduct + applypaydeduct)).toFixed(2);
                        fetch(`https://habibaahmedm-002-site10.atempurl.com/api/Charge2Api/${option}`)
                            .then(response => response.json())
                            .then(data => {

                                let chargededuct = parseInt(data);
                                document.getElementById("appprofitminusegatespercent").innerText = (appprofitminusvat - (madadeduct + mastervisadeduct + applypaydeduct + chargededuct)).toFixed(2);

                            })
                            .catch(error => {
                                console.error("Error fetching data:", error);

                            })

                    })
                    .catch(error => {
                        console.error("Error fetching data:", error);
                    });

            }
            else
            {
                updateTableDate1();
            }
          

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
    // Make an API call to fetch data based on the selected option
}


function bringSettingData()
{
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SettingApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            valueAddedTax = parseInt(data.valueAddedTax);
            appProfitPercent = parseInt(data.appProfitPercent);
           


        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });

}

function updateTableWithoutParameter() {
    let valueAddedTax;
    let appProfitPercent;
    //bringSettingData();
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SettingApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            valueAddedTax = parseInt(data.valueAddedTax);
            appProfitPercent = parseInt(data.appProfitPercent);

            fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SalesPerLawyerApi/`)
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
                    let docSum = 0;
                    let serviceincome = 0;
                    let appprofitmargin = 0;
                    let serviceincomeminusapprofit = 0;
                    let vatofappprofit = 0;
                    let appprofitminusvat = 0;
                    let applypaydeduct = 0;
                    let madadeduct = 0;
                    let mastervisadeduct = 0;
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



                        column1.innerText = rowData.consultingDateAndTime;
                        column2.innerText = rowData.consultingType;
                        column3.innerText = rowData.createdDate;
                        column4.innerText = rowData.createdBy; // المبلغ المدفوع للتفويض
                        column5.innerText = rowData.theConsultingPaidValue;
                        column6.innerText = rowData.theDocumentationPaidValue;
                        column7.innerHTML = `<a href="/Admin/ConsultingEstablish/Form?id=${rowData.consultingId}"  class='btn btn-info text-white' style='cursor:pointer'> تفاصيل</a >`


                        row.appendChild(column1);
                        row.appendChild(column2);
                        row.appendChild(column3);
                        row.appendChild(column4);
                        row.appendChild(column5);
                        row.appendChild(column6);
                        row.appendChild(column7);
                        tbody.appendChild(row);
                        if (rowData.theConsultingPaidValue !== null) {
                            sum = sum + parseInt(rowData.theConsultingPaidValue);
                            if (rowData.paymentGateTitle === "بطاقات مدي") {

                                madadeduct = (madadeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                                
                            }
                            else if (rowData.paymentGateTitle === "بطاقات فيزا و ماستر كارد")
                            {
                                mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                            else if (rowData.paymentGateTitle === "apple pay") {
                                applypaydeduct = (applypaydeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                        }
                        if (rowData.createdBy !== null) {
                            delSum = delSum + parseInt(rowData.createdBy);
                            if (rowData.paymentGateTitleDelegation === "بطاقات مدي") {
                               
                                madadeduct = (madadeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));
                                
                            }
                            else if (rowData.paymentGateTitleDelegation === "بطاقات فيزا و ماستر كارد") {
                               
                                mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));
                                
                            }
                            else if (rowData.paymentGateTitleDelegation === "apple pay") {
                               
                                applypaydeduct = (applypaydeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));
                            }

                        }
                        if (rowData.theDocumentationPaidValue !== null) {
                            docSum = docSum + parseInt(rowData.theDocumentationPaidValue);
                            if (rowData.paymentGateTitle === "بطاقات مدي") {

                                madadeduct = (madadeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));

                            }
                            else if (rowData.paymentGateTitle === "بطاقات فيزا و ماستر كارد") {
                                mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                            else if (rowData.paymentGateTitle === "apple pay") {
                                applypaydeduct = (applypaydeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                        }
                       
                        count++;
                    });
                   
                    serviceincome = sum + delSum + docSum;
                    appprofitmargin = (appProfitPercent * (serviceincome / 100)).toFixed(2);

                    serviceincomeminusapprofit = (serviceincome - appprofitmargin).toFixed(2);
                    vatofappprofit = ((appprofitmargin * valueAddedTax) / 100).toFixed(2);
                    appprofitminusvat = (appprofitmargin - vatofappprofit).toFixed(2);
                    document.getElementById("serviceincome").innerText = serviceincome;
                    document.getElementById("serviceno").innerText = count;
                    document.getElementById("appprofitmargin").innerText = appprofitmargin;
                    document.getElementById("serviceincomeminusapprofit").innerText = serviceincomeminusapprofit;
                    document.getElementById("vatofappprofit").innerText = vatofappprofit;
                    document.getElementById("appprofitminusvat").innerText = appprofitminusvat;
                    document.getElementById("madapercent").innerText = madadeduct.toFixed(2);
                    document.getElementById("mastervisapercent").innerText = mastervisadeduct.toFixed(2);
                    document.getElementById("applypaypercent").innerText = applypaydeduct.toFixed(2);
                    document.getElementById("appprofitminusegatespercent").innerText = (appprofitminusvat - (madadeduct + mastervisadeduct + applypaydeduct)).toFixed(2);
                    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/ChargeApi/`)
                        .then(response => response.json())
                        .then(data => {

                            let chargededuct = parseInt(data);
                            document.getElementById("appprofitminusegatespercent").innerText = (appprofitminusvat - (madadeduct + mastervisadeduct + applypaydeduct + chargededuct)).toFixed(2);

                        })
                        .catch(error => {
                            console.error("Error fetching data:", error);

                        })
                    
                })
                .catch(error => {
                    console.error("Error fetching data:", error);
                });

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
    // Make an API call to fetch data based on the selected option
   
}



function updateTableDate1() {

    let valueAddedTax;
    let appProfitPercent;
    const option = document.getElementById("lawyer").value;
    const optionDate1 = document.getElementById("exampleInputPassword1").value;
    const optionDate2 = document.getElementById("exampleInputPassword2").value;
    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SettingApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            valueAddedTax = parseInt(data.valueAddedTax);
            appProfitPercent = parseInt(data.appProfitPercent);

            fetch(`https://habibaahmedm-002-site10.atempurl.com/api/SalesPerLawyerApi/${option}/${optionDate1}/${optionDate2}`)
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
                    let docSum = 0;
                    let serviceincome = 0;
                    let appprofitmargin = 0;
                    let serviceincomeminusapprofit = 0;
                    let vatofappprofit = 0;
                    let appprofitminusvat = 0;
                    let applypaydeduct = 0;
                    let madadeduct = 0;
                    let mastervisadeduct = 0;
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



                        column1.innerText = rowData.consultingDateAndTime;
                        column2.innerText = rowData.consultingType;
                        column3.innerText = rowData.createdDate;
                        column4.innerText = rowData.createdBy; // المبلغ المدفوع للتفويض
                        column5.innerText = rowData.theConsultingPaidValue;
                        column6.innerText = rowData.theDocumentationPaidValue;
                        column7.innerHTML = `<a href="/Admin/ConsultingEstablish/Form?id=${rowData.consultingId}"  class='btn btn-info text-white' style='cursor:pointer'> تفاصيل</a >`


                        row.appendChild(column1);
                        row.appendChild(column2);
                        row.appendChild(column3);
                        row.appendChild(column4);
                        row.appendChild(column5);
                        row.appendChild(column6);
                        row.appendChild(column7);
                        tbody.appendChild(row);
                        if (rowData.theConsultingPaidValue !== null) {
                            sum = sum + parseInt(rowData.theConsultingPaidValue);
                            if (rowData.paymentGateTitle === "بطاقات مدي") {

                                madadeduct = (madadeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));

                            }
                            else if (rowData.paymentGateTitle === "بطاقات فيزا و ماستر كارد") {
                                mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                            else if (rowData.paymentGateTitle === "apple pay") {
                                applypaydeduct = (applypaydeduct + ((parseInt(rowData.theConsultingPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                        }
                        if (rowData.createdBy !== null) {
                            delSum = delSum + parseInt(rowData.createdBy);
                            if (rowData.paymentGateTitleDelegation === "بطاقات مدي") {

                                madadeduct = (madadeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));

                            }
                            else if (rowData.paymentGateTitleDelegation === "بطاقات فيزا و ماستر كارد") {

                                mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));

                            }
                            else if (rowData.paymentGateTitleDelegation === "apple pay") {

                                applypaydeduct = (applypaydeduct + ((parseInt(rowData.createdBy) * rowData.paymentGatePercentDelegation) / 100));
                            }

                        }
                        if (rowData.theDocumentationPaidValue !== null) {
                            docSum = docSum + parseInt(rowData.theDocumentationPaidValue);
                            if (rowData.paymentGateTitle === "بطاقات مدي") {

                                madadeduct = (madadeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));

                            }
                            else if (rowData.paymentGateTitle === "بطاقات فيزا و ماستر كارد") {
                                mastervisadeduct = (mastervisadeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                            else if (rowData.paymentGateTitle === "apple pay") {
                                applypaydeduct = (applypaydeduct + ((parseInt(rowData.theDocumentationPaidValue) * rowData.paymentGatePercent) / 100));
                            }
                        }

                        count++;
                    });

                    serviceincome = sum + delSum + docSum;
                    appprofitmargin = (appProfitPercent * (serviceincome / 100)).toFixed(2);

                    serviceincomeminusapprofit = (serviceincome - appprofitmargin).toFixed(2);
                    vatofappprofit = ((appprofitmargin * valueAddedTax) / 100).toFixed(2);
                    appprofitminusvat = (appprofitmargin - vatofappprofit).toFixed(2);
                    document.getElementById("serviceincome").innerText = serviceincome;
                    document.getElementById("serviceno").innerText = count;
                    document.getElementById("appprofitmargin").innerText = appprofitmargin;
                    document.getElementById("serviceincomeminusapprofit").innerText = serviceincomeminusapprofit;
                    document.getElementById("vatofappprofit").innerText = vatofappprofit;
                    document.getElementById("appprofitminusvat").innerText = appprofitminusvat;
                    document.getElementById("madapercent").innerText = madadeduct.toFixed(2);
                    document.getElementById("mastervisapercent").innerText = mastervisadeduct.toFixed(2);
                    document.getElementById("applypaypercent").innerText = applypaydeduct.toFixed(2);
                    document.getElementById("appprofitminusegatespercent").innerText = (appprofitminusvat - (madadeduct + mastervisadeduct + applypaydeduct)).toFixed(2);
                    fetch(`https://habibaahmedm-002-site10.atempurl.com/api/Charge2Api/${option}/${optionDate1}/${optionDate2}`)
                        .then(response => response.json())
                        .then(data => {

                            let chargededuct = parseInt(data);
                            document.getElementById("appprofitminusegatespercent").innerText = (appprofitminusvat - (madadeduct + mastervisadeduct + applypaydeduct + chargededuct)).toFixed(2);

                        })
                        .catch(error => {
                            console.error("Error fetching data:", error);

                        })

                })
                .catch(error => {
                    console.error("Error fetching data:", error);
                });

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
    // Make an API call to fetch data based on the selected option




}