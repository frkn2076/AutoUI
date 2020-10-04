const preTextGet = 'Enter endpoint parameters:';
const preTextPost = 'Enter json request:';
const rootEndpoint = 'https://localhost:44348';
function MyPost(endpoint, id) {
    var request = document.getElementById(id).value;
    var url = rootEndpoint + '/' + endpoint;
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=UTF-8'
        },
        'type': 'POST',
        'url': url,
        'data': request,
        'dataType': 'json',
        'success': function (response, status) {
            alert("Endpoint: " + url + "\nStatus: " + status + "\nResponse: " + response);
        },
        'error': function () {
            alert("Endpoint: " + url + "\nStatus: fail" + "\nSomething went wrong");
        }
    });
}

function MyGet(endpoint) {
    var url = rootEndpoint + '/' + endpoint;
    $.get(url, function (response, status) {
        alert("Endpoint: " + url + "\nStatus: " + status + "\nResponse: " + response);
    }).fail(function () {
        alert("Endpoint: " + url + "\nStatus: fail" + "\nSomething went wrong");
    });
}

window.onload = function () {
    $.get("https://localhost:44348/UIDrawer", function (response) {
        let body = '<table style="width:32%; text-align:left;">';
        for (var i = 0; i < response.length; i++) {
            for (var j = 0; j < response[i].methods.length; j++) {
                body += '<tr>';
                let endpoint = response[i].methods[j].endpoint;
                let isGet = response[i].methods[j].serviceType == 0;
                let buttonName = response[i].methods[j].name;
                let id = i.toString().concat(j); 
                if (isGet) {
                    body += '<th> <p>"GET"</p> </th>';
                    body += '<th> <br> </br> </th>';
                    body += '<th> <br> </br> </th>';
                    body += '<th> <button onclick = "MyGet(\'' + endpoint + '\'' + ')" style="min-width: 120px;">' + buttonName + '</button > </th>';
                } else {
                    body += '<th> <p>"POST"</p> </th>';
                    body += '<th>' + preTextPost + '</th>';
                    body += '<th> <input type="text" name="name" id="' + i + j + '">' + '</th>';
                    body += '<th> <button  onclick="MyPost(\'' + endpoint + '\',\'' + id + '\' )" style="min-width: 120px;">' + buttonName + '</button > </th>';
                }
                body += '</tr>';
            }
        }
        body += '</table>';
        document.body.innerHTML = body;
    }).error(function () {
        alert("Something went wrong");
    });
}   
