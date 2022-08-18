$(document).ready(() => {
    //Evento click para obtener archivo XML (No lo exporta)
    $("#toXml").click(function (event) {
        var table = $("#reportTable").tableToJSON(); //Convirtiendo los registros de la tabla a formato JSON
        var inputJSON = JSON.stringify(table);
        var output = eval("OBJtoXML("+inputJSON+");") //Declarando una variable para invocar el metodo
        alert(output);
    });

    //Evento click para obtener y exportar archivo JSON
    $("#toJson").click(function (event) {
        var table = $("#reportTable").tableToJSON(); //Convirtiendo los registros de la tabla a formato JSON  
        downloadObjectAsJson(table, "top10Covid"); //Invocando el metodo

    });

    //Evento click para obtener y exportar archivo XLS
    $("#toCsv").click(function (event) {
        $("#reportTable").table2excel({ //Convirtiendo los registros de la tabla a formato CSV
            exclude: ".noExport",
            name: "Top 10 COVID cases report",
            filename: "Top 10 COVID cases report",
            fileext: ".xls"
        })
    });

    //Funcion para obtener archivo XML (No lo exporta)
    function OBJtoXML(obj) {
        var xml = '';
        for (var prop in obj) {
            xml += obj[prop] instanceof Array ? '' : "<" + prop + ">";
            if (obj[prop] instanceof Array) {
                for (var array in obj[prop]) {
                    xml += "<" + prop + ">";
                    xml += OBJtoXML(new Object(obj[prop][array]));
                    xml += "</" + prop + ">";
                }
            } else if (typeof obj[prop] == "object") {
                xml += OBJtoXML(new Object(obj[prop]));
            } else {
                xml += obj[prop];
            }
            xml += obj[prop] instanceof Array ? '' : "</" + prop + ">";
        }
        var xml = xml.replace(/<\/?[0-9]{1,}>/g, '');
        return xml
    }

    //Funcion para obtener y exportar archivo JSON
    function downloadObjectAsJson(exportObj, exportName) {
        var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj));
        var downloadAnchorNode = document.createElement('a');
        downloadAnchorNode.setAttribute("href", dataStr);
        downloadAnchorNode.setAttribute("download", exportName + ".json");
        document.body.appendChild(downloadAnchorNode); // required for firefox
        downloadAnchorNode.click();
        downloadAnchorNode.remove();
    }
});
