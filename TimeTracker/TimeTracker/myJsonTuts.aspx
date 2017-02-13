<head>
    <title>Title</title>
    <script src="latestJs_1.11/jquery.min.js"></script>
    <script>
        //*
        $("#myButton").on("click", function (e) {
            e.preventDefault();
            var aData = [];
            aData[0] = $("#ddlSelectYear").val();
            $("#contentHolder").empty();
            var jsonData = JSON.stringify({ aData: aData });
            $.ajax({
                type: "POST",
                //getListOfCars is my webmethod   
                url: "WebService.asmx/getListOfCars",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json", // dataType is json format
                success: OnSuccess,
                error: OnErrorCall
            });

            function OnSuccess(response) {
                console.log(response.d)
            }
            function OnErrorCall(response) { console.log(error); }
        });
        //*
    </script>
</head>
<body>
    <form runat="server">
        <div>
            <select id="ddlSelectYear">
                <option>2014</option>
                <option>2015</option>
            </select>
            <button id="myButton">Get Car Lists</button>
            <div id="contentHolder"></div>
        </div>
    </form>
</body>

