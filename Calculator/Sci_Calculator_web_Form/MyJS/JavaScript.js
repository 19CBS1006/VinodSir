var historyStack = [];
var memoryStack = [];
var result = [];
var anyBracket = false;
var backendIndex = 0;
var state = 0;
var resultIndex = 0;
var decimalIsSet = false;
var noOfOpenBrackets = 0;
var finalResult;
var IsSolvable = false;
var newresult = false;
var mode = "sci";
var internalmode = 0;
var functionval = false;
var element = document.getElementById("currentTextArea");
var currentValue = element.innerText;
var tillNowElement = document.getElementById("tillnowtextArea");
var unitElements = document.getElementById('units');
var unit;
var inputWaitMethods = [];
var inputWaitMethodType = [];
var inputWaitMethodSolveable = [];
var inputWaitMethodFirstVal = [];
var IsinputWaitMethodStarting = [];
var IsModeC = false;
var ValueIsChanged = false;
var previousValue;
var IsclosedBracket = false;
document.addEventListener('DOMContentLoaded', function () {
    var unit = unitElements.value;
    unitElements.addEventListener('change', function () {
        var selectedValue = unitElements.value;
        unit = selectedValue;
    });
    var currenthistoryelements = document.querySelectorAll("#history");
    if (currenthistoryelements.length < 2) {
        var currenthistory = document.getElementById("historyPlaceBefore");
        currenthistory.innerText = "There is nothing in History as of yet";
    }
    else {
        var currenthistory = document.getElementById("historyPlaceBefore");
        currenthistory.innerText = " ";
    }
    var currentmemoryelements = document.querySelectorAll("#memory");
    if (currentmemoryelements.length < 2) {
        var currentmemory = document.getElementById("memoryPlaceBefore");
        currentmemory.innerText = "There is nothing in Memory as of yet";
    }
    else {
        var currentmemory = document.getElementById("memoryPlaceBefore");
        currentmemory.innerText = " ";
    }
    numberKeys();
    document.getElementById("add").addEventListener("click", function () {
        oprs('+');
    });
    document.getElementById("subtract").addEventListener("click", function () {
        oprs('-');
    });
    document.getElementById("multiply").addEventListener("click", function () {
        oprs('*');
    });
    document.getElementById("divide").addEventListener("click", function () {
        oprs('/');
    });
    document.getElementById("open").addEventListener("click", function () {
        oprs('(');
    });
    document.getElementById("close").addEventListener("click", function () {
        oprs(')');
    });

});
document.addEventListener("keydown",
    (event) => {
        element = document.getElementById("currentTextArea");
        currentValue = element.innerText;
        if (event.key == "Backspace") {
            backspace();
        }
        else if (event.key == "Delete") {
            ce();
        }
        else if (event.key == "Enter" || event.key == "=") {
            equal();
        }
        else if (event.key == "+" || event.key == "-" || event.key == "*" || event.key == "/") {
            if (inputWaitMethods[inputWaitMethods.length-1] == true && inputWaitMethodSolveable[inputWaitMethodSolveable.length - 1] == 0) {
                inputWaitMethods[inputWaitMethods.length - 1] = false;
                if (inputWaitMethodType[inputWaitMethodType.length - 1] == '0') {
                    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')') {
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = Math.pow(parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]), parseFloat(result[result.length - 1]));
                    }
                    else {
                        tillNowElement.innerText = tillNowElement.innerText + currentValue + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = Math.pow(parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]), parseFloat(currentValue));
                    }
                    inputWaitMethodFirstVal.pop();
                    inputWaitMethodSolveable.pop();
                    inputWaitMethodType.pop();
                    inputWaitMethods.pop();
                    element.innerText = currentValue;
                    result[result.length - 1] = currentValue;
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                    state = "0";
                    decimalIsSet = false;
                    ValueIsChanged = true;
                    IsSolvable = true;
                    if ((result.length - parseFloat(noOfOpenBrackets)) > 2 && IsSolvable && result[2] !== undefined && result[result.length - 3] != '(') {
                        console.log(result[resultIndex - 1] + " okey" + result.length);
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " " + i);
                        }*/
                        finalResult = solveOpr(result, resultIndex - 2);
                        element.innerText = finalResult.computedValue;
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " 2nd " + i);
                        }*/
                    }
                    /*console.log("jujubi" + result.length);
                    for (var i = 0; i < result.length; i++) {
                        console.log(result[i] + " " + i);
                    }*/
                }
                else if (inputWaitMethodType[inputWaitMethodType.length - 1] == '1') {
                    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')') {
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = Math.pow(parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]), parseFloat(1 / parseFloat(result[result.length - 1])));
                    }
                    else {
                        tillNowElement.innerText = tillNowElement.innerText + currentValue + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = Math.pow(parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]), parseFloat(1 / parseFloat(currentValue)));
                    }
                    inputWaitMethodFirstVal.pop();
                    inputWaitMethodSolveable.pop();
                    inputWaitMethodType.pop();
                    inputWaitMethods.pop();
                    element.innerText = currentValue;
                    result[result.length - 1] = currentValue;
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                    state = "0";
                    decimalIsSet = false;
                    ValueIsChanged = true;
                    IsSolvable = true;
                    if ((result.length - parseFloat(noOfOpenBrackets)) > 2 && IsSolvable && result[2] !== undefined && result[result.length - 3] != '(') {
                        /*console.log(result[resultIndex - 1] + " okey" + result.length);
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " " + i);
                        }*/
                        finalResult = solveOpr(result, resultIndex - 2);
                        element.innerText = finalResult.computedValue;
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " 2nd " + i);
                        }*/
                    }
                }

                else if (inputWaitMethodType[inputWaitMethodType.length - 1] == '2') {
                    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')') {
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = Math.log(parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1])) / Math.log(parseFloat(result[result.length - 1]));
                    }
                    else {
                        tillNowElement.innerText = tillNowElement.innerText + currentValue + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = Math.log(parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]))/ Math.log(parseFloat(currentValue));
                    }
                    inputWaitMethodFirstVal.pop();
                    inputWaitMethodSolveable.pop();
                    inputWaitMethodType.pop();
                    inputWaitMethods.pop();
                    element.innerText = currentValue;
                    result[result.length - 1] = currentValue;
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                    state = "0";
                    decimalIsSet = false;
                    ValueIsChanged = true;
                    IsSolvable = true;
                    if ((result.length - parseFloat(noOfOpenBrackets)) > 2 && IsSolvable && result[2] !== undefined && result[result.length - 3] != '(') {
                        console.log(result[resultIndex - 1] + " okey" + result.length);
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " " + i);
                        }*/
                        finalResult = solveOpr(result, resultIndex - 2);
                        element.innerText = finalResult.computedValue;
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " 2nd " + i);
                        }*/
                    }
                }
                else if (inputWaitMethodType[inputWaitMethodType.length - 1] == '3') {
                    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')') {
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]) % parseFloat(result[result.length - 1]);
                    }
                    else {
                        tillNowElement.innerText = tillNowElement.innerText + currentValue + `${event.key}`;
                        previousValue = currentValue;
                        currentValue = parseFloat(inputWaitMethodFirstVal[inputWaitMethodFirstVal.length - 1]) % parseFloat(currentValue);
                    }
                    inputWaitMethodFirstVal.pop();
                    inputWaitMethodSolveable.pop();
                    inputWaitMethodType.pop();
                    inputWaitMethods.pop();
                    element.innerText = currentValue;
                    result[result.length - 1] = currentValue;
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                    state = "0";
                    decimalIsSet = false;
                    ValueIsChanged = true;
                    IsSolvable = true;
                    if ((result.length - parseFloat(noOfOpenBrackets)) > 2 && IsSolvable && result[2] !== undefined && result[result.length - 3] != '(') {
                        console.log(result[resultIndex - 1] + " okey" + result.length);
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " " + i);
                        }
                        finalResult = solveOpr(result, resultIndex - 2);
                        element.innerText = finalResult.computedValue;
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " 2nd " + i);
                        }
                    }
                }
            }
            else {
                if (tillNowElement.innerText == "" || newresult) {
                    if (newresult && !functionval) {
                        tillNowElement.innerText = tillNowElement.innerText + currentValue + `${event.key}`;
                    }
                    else if (functionval) {
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        functionval = false;
                    }
                    else {
                        tillNowElement.innerText = currentValue + `${event.key}`;
                    }
                    element.innerText = currentValue;
                    result.push(element.innerText);
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                    state = "0";
                    decimalIsSet = false;
                    newresult = false;
                    ValueIsChanged = false;

                }
                else {
                    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')' && !functionval) {
                        console.log(resultIndex);
                        result.push(`${event.key}`);
                        resultIndex = result.length;
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i]);
                        }*/
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        IsSolvable = false;
                    }
                    else if (functionval) {
                        tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                        functionval = false;
                    }
                    else {
                        tillNowElement.innerText = tillNowElement.innerText + `${currentValue}` + `${event.key}`;
                    }
                    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '+') {
                        IsSolvable = true;
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " ok1 " + i);
                        }
                        console.log(ValueIsChanged + " " + result.length);
                        resultIndex = result.length;
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " + "+ i);
                        }*/
                        if (result.length > 1) {
                            if(result[resultIndex - 1] == '*') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) * parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                            else if (result[resultIndex - 1] == '/') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) / parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                            else if (result[resultIndex - 1] == '-') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) - parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                            else if (result[resultIndex - 1] == '+') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) + parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                        }
                        element.innerText = result[resultIndex - 2];
                        console.log(currentValue + " " + result[resultIndex - 2]);
                        resultIndex = result.length;                        
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " ok " + i);
                        }
                        //element.innerText = parseFloat(result[resultIndex]) + parseFloat(parseFloat(currentValue));
                    }
                    else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '-') {
                        IsSolvable = true;
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " ok1 " + i);
                        }
                        console.log(ValueIsChanged + " " + result.length);
                        resultIndex = result.length;
                        if (result.length > 1) {
                            if (result[resultIndex - 1] == '*') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) * parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }                                
                            }
                            else if (result[resultIndex - 1] == '/') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) / parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                            else if (result[resultIndex - 1] == '-') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) - parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                            else if (result[resultIndex - 1] == '+') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) + parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                }
                            }
                        }
                        element.innerText = result[resultIndex - 2];
                        console.log(currentValue + " " + result[resultIndex - 2]);
                        resultIndex = result.length;
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " ok " + i);
                        }
                        //element.innerText = parseFloat(result[resultIndex]) - parseFloat(parseFloat(currentValue));
                    }
                    else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '*') {
                        IsSolvable = false;
                        if (result.length > 1) {
                            /*console.log("okokokok here2 " + index + " " + result[resultIndex - 1]);*/
                            resultIndex = result.length;
                            /*for (var i = 0; i < result.length; i++) {
                                console.log(result[i] + " okok "+ i);
                            }*/
                            if (result[resultIndex - 1] == '*') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) * parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                    element.innerText = result[resultIndex - 2];
                                }
                            }
                            else if (result[resultIndex - 1] == '/') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) / parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                    element.innerText = result[resultIndex - 2];
                                }
                                
                            }
                            else if (result[resultIndex - 1] == '+' || result[resultIndex - 1] == '-') {
                                if (ValueIsChanged) {
                                }
                                result.push(currentValue);
                                result.push(`${event.key}`);
                                resultIndex = result.length;
                                element.innerText = currentValue;
                                
                            }
                            /*console.log("mul after");
                            for (var i = 0; i < result.length; i++) {
                                console.log(result[i] + " okok " + i);
                            }*/
                        }
                    }
                    else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '/') {
                        IsSolvable = false;
                        if (result.length > 1) {
                            /*var index = parseFloat(resultIndex) - 1;
                            console.log("okokokok here2 " + index + " " + result[resultIndex - 1]);
                            for (var i = 0; i < result.length; i++) {
                                console.log(result[i]);
                            }*/
                            resultIndex = result.length;
                            if (result[resultIndex - 1] == '*') {
                                console.log("boom boom " + ValueIsChanged);
                                if (ValueIsChanged) {
                                    console.log("boom boom");
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) * parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                    element.innerText = result[resultIndex - 2];
                                }
                            }
                            else if (result[resultIndex - 1] == '/') {
                                if (ValueIsChanged) {
                                    result[resultIndex - 2] = parseFloat(result[resultIndex - 2]) / parseFloat(currentValue);
                                    result[resultIndex - 1] = `${event.key}`;
                                    element.innerText = result[resultIndex - 2];
                                }
                                
                            }
                            else if (result[resultIndex - 1] == '+' || result[resultIndex - 1] == '-') {
                                if (ValueIsChanged) {
                                    result.push(currentValue);
                                    result.push(`${event.key}`);
                                    resultIndex = result.length;
                                    /*for (var i = 0; i < result.length; i++) {
                                        console.log(result[i]);
                                    }*/
                                }
                                    element.innerText = currentValue;
                                
                            }
                        }
                    }
                    if ((result.length - parseFloat(noOfOpenBrackets)) > 2 && IsSolvable && result[2] !== undefined && result[result.length - 3] != '(') {
                        console.log(result[resultIndex - 1] + " okey" + result.length);
                        /*for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " " + i);
                        }*/
                        finalResult = solveOpr(result, resultIndex - 2);
                        element.innerText = finalResult.computedValue;
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " 2nd " + i);
                        }
                    }
                    state = "0";
                    decimalIsSet = false;
                }
            }
        }
        else if (event.key == '1' || event.key == '2' || event.key == '3' || event.key == '4' || event.key == '5' || event.key == '6' || event.key == '7' || event.key == '8' || event.key == '9' || event.key == '0') {
            if (state == '0') {
                element.innerText = `${event.key}`;
                state = element.innerText;
            }
            else {
                element.innerText = currentValue + `${event.key}`;
                state = element.innerText;
            }
            ValueIsChanged = true;
        }
        else if (event.key == '.') {
            if (!decimalIsSet) {
                if (state == '0') {
                    element.innerText = '0' + `${event.key}`;
                    state = element.innerText;
                }
                else {
                    element.innerText = currentValue + `${event.key}`;
                    state = element.innerText;
                }
                decimalIsSet = true;
            }
        }

        else if (event.key == '(') {
            if (inputWaitMethods[inputWaitMethods.length - 1] === true) {
                inputWaitMethodSolveable[inputWaitMethodSolveable.length - 1] = inputWaitMethodSolveable[inputWaitMethodSolveable.length - 1] + 1;
            }
            newresult = true;
            anyBracket = true;
            if (tillNowElement.innerText.length > 0) {
                if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '+' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '-' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '*' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '/' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '(') {
                    tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                    noOfOpenBrackets = noOfOpenBrackets + 1;
                    element.innerText = "0";
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                }
                else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '^') {
                    tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                    noOfOpenBrackets = noOfOpenBrackets + 1;
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                    element.innerText = "0";
                    state = "0";
                }
                else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')') {
                    element.innerText = "0";
                    state = "0";
                    decimalIsSet = false;
                    tillNowElement.innerText = tillNowElement.innerText + 'x' + `${event.key}`;
                    noOfOpenBrackets = noOfOpenBrackets + 1;
                    result.push(`${event.key}`);
                    resultIndex = result.length;
                }
            }
            else {
                if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '' && element.innerText != '0') {
                    element.innerText = "0";
                    state = "0";
                    decimalIsSet = false;
                    result.push(currentValue);
                    result.push('*');
                    tillNowElement.innerText = currentValue + 'x' + `${event.key}`;
                    result.push(`${event.key}`);
                    noOfOpenBrackets = noOfOpenBrackets + 1;
                    resultIndex = result.length;
                }
                else {
                    tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                    result.push(`${event.key}`);
                    noOfOpenBrackets = noOfOpenBrackets + 1;
                    resultIndex = result.length;
                }
                
            }
            console.log("( index " + resultIndex);
        }
        else if (event.key == ')') {
            if (inputWaitMethods[inputWaitMethods.length-1] === true && inputWaitMethodSolveable[inputWaitMethodSolveable.length - 1] > 0) {
                inputWaitMethodSolveable[inputWaitMethodSolveable.length - 1] = inputWaitMethodSolveable[inputWaitMethodSolveable.length - 1] - 1;
            }
            if (noOfOpenBrackets > 0) {
                if (result[resultIndex] == currentValue) {
                    tillNowElement.innerText = tillNowElement.innerText + `${event.key}`;
                }
                else {
                    tillNowElement.innerText = tillNowElement.innerText + currentValue + `${event.key}`;
                }
                result.push(currentValue);  
                resultIndex = result.length;
                noOfOpenBrackets = noOfOpenBrackets - 1;
                if ((result.length - parseFloat(noOfOpenBrackets)) > 2) {
                    for (var i = 0; i < result.length; i++) {
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] + " " + i);
                        }
                        IsclosedBracket = true;
                        finalResult = solveOpr(result, result.length - 1);
                        console.log("after " + finalResult.computedValue + " " + finalResult.index);
                        for (var i = 0; i < result.length; i++) {
                            console.log(result[i] +" " + i);
                        }
                        for (var a = finalResult.index; a < result.length; a++){
                            result.pop();
                        }
                        element.innerText = finalResult.computedValue;
                        resultIndex = finalResult.index;
                        console.log("added index " + resultIndex);
                        result[resultIndex] = finalResult.computedValue;
                        ValueIsChanged = false;
                        IsclosedBracket = false;
                    }
                }
            }
        }
        else {
            /*if (state == '0') {
                element.innerText = `${event.key}`;
                state = element.innerText;
            }
            else {
                element.innerText = currentValue + `${event.key}`;
                state = element.innerText;
            }*/
        }
    },
    true,
);

function solveOpr(result, index) {
    var Result;
    var resultIndex;
    for (var i = index; i >= 2; i = i - 2) {
        console.log("start " + i + " " + result[i]);
        for (var j = 0; j < result.length; j++) {
            console.log(result[j] + " " + j);
        }
        var oprnd1 = result[i];
        var opr = result[i - 1];
        var oprnd2 = result[i - 2];
        if (opr == '+') {
            result[i - 2] = parseFloat(oprnd2) + parseFloat(oprnd1)
        }
        else if (opr == '-') {
            result[i - 2] = parseFloat(oprnd2) - parseFloat(oprnd1)
        }
        else if (opr == '*') {
            result[i - 2] = parseFloat(oprnd2) * parseFloat(oprnd1)
        }
        else if (opr == '/') {
            result[i - 2] = parseFloat(oprnd2) / parseFloat(oprnd1)
        }
        else if (opr == '(') {
            for (var j = 0; j < result.length; j++) {
                console.log(result[j] + " " + j);
            }
            if (IsclosedBracket) {
                result.splice(i - 1, 1);
                return {
                    computedValue: result[i - 1],
                    index: i - 1
                }
            }
            else {
                return {
                    computedValue: result[i],
                    index: i
                }
            }
        }
        Result = result[i - 2];
        resultIndex = i - 2;
        result.splice(i - 1, 2);
    }
    /*for (var a = 0; a < result.length; a++) {
        result.pop(); //12((1+1)*1+1)+20+ =>undefined
    }*/
    if (result[0] == '(') {
        result.splice(0, 1);
        for (var j = 0; j < result.length; j++) {
            console.log(result[j] + " " + j);
        }
        return {
            computedValue: Result,
            index: 0
        }
    }
    for (var j = 0; j < result.length; j++) {
        console.log(result[j] + " " + j);
    }
    return {
        computedValue: Result,
        index: resultIndex
    }
}

let intervalId;
function monitorOutput() {
    let output = document.getElementById("currentTextArea");
    if (output.innerText == "NaN" || output.innerText == "Infinity" || output.innerText == "16331239353195370") {
        let errorEvent = new CustomEvent('errorOutput');
        output.innerText = "Invalid Input";
        document.dispatchEvent(errorEvent);
        clearInterval(intervalId);
    }
    /*if (output.innerText != previousValue) {
        let valueChangedEvent = new CustomEvent('textBoxValuechange');
        document.dispatchEvent(valueChangedEvent);
        clearInterval(intervalId);
    }*/
}
intervalId = setInterval(monitorOutput, 100);

document.addEventListener('textBoxValuechange', function (event) {
    ValueIsChanged = true;
    console.log("triggered");
})
document.addEventListener('errorOutput', function (event) {
    let allButtons = document.querySelectorAll("button");
    allButtons.forEach(function (button) {
        //console.log("done");
        button.disabled = true;
        button.style.pointerEvents = "none";
        button.style.backgroundColor = "rgba(44, 45, 45, 0.2)";
    });
    let necessaryButtons = document.querySelectorAll(".num");
    necessaryButtons.forEach(function (nb) {
        //console.log("done2");
        nb.disabled = false;
        nb.style.pointerEvents = "auto";
        nb.style.backgroundColor = "rgba(49, 49, 49,1)";
    })
    let rightBtns = document.querySelectorAll("#rightbtns button");
    rightBtns.forEach(function (btn) {
        //console.log("done");
        btn.disabled = false;
        btn.style.pointerEvents = "auto";
        btn.style.backgroundColor = "rgba(44, 45, 45, 1)";
    });
});

function secondTogggler() {
    let buttons = document.querySelectorAll("#keypad2 div button");
    buttons.forEach(function (button) {
        if (button.innerHTML == "x<sup>2</sup>") {
            button.innerHTML = "x<sup>3</sup>";
            internalmode = 1;
        } else if (button.innerHTML == "x<sup>3</sup>") {
            button.innerHTML = "x<sup>2</sup>";
        } else if (button.innerHTML == "sqrt") {
            button.innerHTML = "cbrt";
        } else if (button.innerHTML == "cbrt") {
            button.innerHTML = "sqrt";
        } else if (button.innerHTML == "x<sup>y</sup>") {
            button.innerHTML = "x root y";
        } else if (button.innerHTML == "x root y") {
            button.innerHTML = "x<sup>y</sup>";
        } else if (button.innerHTML == "10<sup>x</sup>") {
            button.innerHTML = "2<sup>x</sup>";
        } else if (button.innerHTML == "2<sup>x</sup>") {
            button.innerHTML = "10<sup>x</sup>";
        } else if (button.innerHTML == "log") {
            button.innerHTML = "log<sub>y</sub>x";
        } else if (button.innerHTML == "log<sub>y</sub>x") {
            button.innerHTML = "log";
        } else if (button.innerHTML == "ln") {
            button.innerHTML = "e<sup>x</sup>";
        } else if (button.innerHTML == "e<sup>x</sup>") {
            button.innerHTML = "ln";
        }
    });
}

function equal() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    /*console.log("final " + finalResult.computedValue + " " + finalResult.index);
            for (var i = 0; i < result.length; i++) {
                console.log(result[i]);
            }*/
    if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == ')') {
        finalResult = solveOpr(result, resultIndex);
        tillNowElement.innerText = tillNowElement.innerText;
        result[0] = finalResult.computedValue;
        /*console.log(finalResult.index);
        console.log(finalResult.computedValue);*/
        element.innerText = result[0];
    }
    else if (result.length === 1) {
        element.innerText = result[0];
    }
    else {
        result.push(currentValue);
        resultIndex = result.length;

        if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '+') {
            element.innerText = result[resultIndex];
        }
        else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '-') {
            element.innerText = result[resultIndex];
        }
        else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '*') {
            element.innerText = result[resultIndex];
        }
        else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '/') {
            element.innerText = result[resultIndex];
        }
        tillNowElement.innerText = tillNowElement.innerText + `${currentValue}`;
        //console.log("finalchg " + finalResult.computedValue + " " + finalResult.index);
        /*for (var i = 0; i < result.length; i++) {
            console.log(result[i]);
        }*/
        //console.log(result[resultIndex] + " " + resultIndex);
        if (result[resultIndex] === undefined) {
            finalResult = solveOpr(result, resultIndex - 1);
        }
        else {
            finalResult = solveOpr(result, resultIndex);
        }

        result[0] = finalResult.computedValue;
        /*console.log(finalResult.index);
        console.log(finalResult.computedValue);*/
        element.innerText = result[0];
    }
    state = "0";
    decimalIsSet = false;
    element.style.fontWeight = "bold";
    element.style.fontSize = "26px";
    historyStack.push(tillNowElement.innerText);
    historyPopulate(tillNowElement.innerText);
}

function oprs(keys) {
    if (keys == '+') {
        var event = new KeyboardEvent('keydown', {
            key: '+',
            keyCode: 187,
            code: 'Equal',
            which: 187,
            shiftKey: true,
        });
        document.dispatchEvent(event);
    }
    else if (keys == '-') {
        var event = new KeyboardEvent('keydown', {
            key: '-',
            keyCode: 189,
            code: 'Minus',
            which: 189,
            shiftKey: true,
        });
        document.dispatchEvent(event);
    }
    else if (keys == '*') {
        var event = new KeyboardEvent('keydown', {
            key: '*',
            keyCode: 56,
            code: 'Digit8',
            which: 56,
            shiftKey: true,
        });
        document.dispatchEvent(event);
    }
    else if (keys == '/') {
        var event = new KeyboardEvent('keydown', {
            key: '/',
            keyCode: 191,
            code: 'Slash',
            which: 191,
            shiftKey: true,
        });
        document.dispatchEvent(event);
    }
    else if (keys == '(') {
        var event = new KeyboardEvent('keydown', {
            key: '(',
            keyCode: 57,
            code: 'Digit9',
            which: 57,
            shiftKey: true,
        });
        document.dispatchEvent(event);
    }
    else if (keys == ')') {
        var event = new KeyboardEvent('keydown', {
            key: ')',
            keyCode: 48,
            code: 'Digit0',
            which: 48,
            shiftKey: true,
        });
        document.dispatchEvent(event);
    }
}
function historyPopulate(historyElement) {
    const newHist = document.createElement("p");
    const newContent = document.createTextNode(historyElement);
    newHist.appendChild(newContent);
    const placeBefore = document.getElementById("historyPlaceBefore");
    document.getElementById("history").insertBefore(newHist, placeBefore);
    document.getElementById("historyPlaceBefore").innerText = "";

}

function memoryStore() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    console.log("stack puching " + currentValue);
    memoryStack.push(currentValue);
    const newMemory = document.createElement("p");
    const newContent = document.createTextNode(currentValue);
    newMemory.appendChild(newContent);
    var memoryDiv = document.getElementById("memory");
    memoryDiv.insertBefore(newMemory, memoryDiv.firstChild);
    document.getElementById("memoryPlaceBefore").innerText = "";
}

function memoryMinus() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    console.log(parseFloat(memoryStack[memoryStack.length - 1]) + " " + parseFloat(currentValue));
    memoryStack[memoryStack.length - 1] = parseFloat(memoryStack[memoryStack.length - 1]) - parseFloat(currentValue);
    var memoryHtmlValues = document.querySelectorAll("#memory p");
    for (var i = 0; i < memoryHtmlValues.length; i++) {
        memoryHtmlValues[i].innerText = memoryStack[memoryStack.length - 1];
        break;
    }
}
function memoryPlus() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    console.log(parseFloat(memoryStack[memoryStack.length - 1]) + " " + parseFloat(currentValue));
    memoryStack[memoryStack.length - 1] = parseFloat(memoryStack[memoryStack.length - 1]) + parseFloat(currentValue);
    var memoryHtmlValues = document.querySelectorAll("#memory p");
    for (var i = 0; i < memoryHtmlValues.length; i++) {
        memoryHtmlValues[i].innerText = memoryStack[memoryStack.length - 1];
        break;
    }
}
function memoryRecall() {
    element = document.getElementById("currentTextArea");
    previousValue = element.innerText;
    ValueIsChanged = true;
    element.innerText = memoryStack[memoryStack.length - 1];


}
function memoryClear() {
    var memoryHtmlValues = document.querySelectorAll("#memory p");
    var memoryDiv = document.getElementById("memory");
    for (var i = 0; i < memoryHtmlValues.length; i++) {
        memoryDiv.removeChild(memoryHtmlValues[0]);
        break;
    }
    memoryStack.pop();
}

function fact() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    tillNowElement.innerText = tillNowElement.innerText + `fact(${currentValue})`;
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.floor(parseFloat(currentValue));
    var factorialResult = 1;
    for (var i = currentValue; i > 0; i--) {
        factorialResult = factorialResult * i;
    }
    element.innerText = factorialResult;
    
}
function dot() {
    if (!decimalIsSet) {
            element.innerText = '0.'; //set the tillnow
            state = element.innerText;
        }
        else {
            element.innerText = currentValue + '.';
            state = element.innerText;
        }
        decimalIsSet = true;
    }

function xsq() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    if (internalmode == '1') {
        
        tillNowElement.innerText = tillNowElement.innerText + '' + currentValue + '^3';
        previousValue = currentValue;
        currentValue = Math.pow(parseFloat(currentValue),3);
        element.innerText = currentValue;
    }
    else {
        tillNowElement.innerText = tillNowElement.innerText + '' + currentValue + '^2';
        previousValue = currentValue;
        currentValue = Math.pow(parseFloat(currentValue), 2);;
        element.innerText = currentValue;
    }
    functionval = true;
    state = 0;
    
}

function pi() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.PI;
    element.innerText = currentValue;
}

function rt() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    if (internalmode == '1') {

        tillNowElement.innerText = tillNowElement.innerText + 'cbrt(' + currentValue + ')';
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.pow(parseFloat(currentValue), 1/3);
        element.innerText = currentValue;
    }
    else {
        tillNowElement.innerText = tillNowElement.innerText + 'sqrt(' + currentValue + ')';
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.pow(parseFloat(currentValue), 1/2);;
        element.innerText = currentValue;
    }
    functionval = true;
    state = 0;
}

function e() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.E;
    element.innerText = currentValue;
}

function ce() {
    
    var text = "0";
    element.innerText = text;
    IsModeC = true;
    var currentname = document.getElementById("CCE");
    if (IsModeC && currentname.innerText == "C") {
        tillNowElement.innerText = "";
        currentname.innerText = 'CE';
        result = [];
        anyBracket = false;
        backendIndex = 0;
        state = 0;
        resultIndex = 0;
        decimalIsSet = false;
        noOfOpenBrackets = 0;
        finalResult;
        IsSolvable = false;
        newresult = false; var internalmode = 0;
        functionval = false;
        var text = "0";
        element.innerText = text;
        element = document.getElementById("currentTextArea");
        currentValue = element.innerText;
        tillNowElement = document.getElementById("tillnowtextArea");
        inputWaitMethods = [];
        inputWaitMethodType = [];
        inputWaitMethodSolveable = [];
        inputWaitMethodFirstVal = [];
        IsinputWaitMethodStarting = [];
        IsModeC = false;
        ValueIsChanged = false;
        previousValue;
    } else {
        currentname.innerText = 'C';
    }

}

function abs() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    tillNowElement.innerText = tillNowElement.innerText + `abs(${currentValue})`;
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.abs(parseFloat(currentValue));
    element.innerText = currentValue;
    functionval = true;
    state = 0;
}

function negate() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    tillNowElement.innerText = tillNowElement.innerText + `negate(${currentValue})`;
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = -1* parseFloat(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
}
function backspace() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    var text = currentValue.substring(0, currentValue.length - 1);
    previousValue = element.innerText;
    element.innerText = text;
}

function inverse() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    tillNowElement.innerText = tillNowElement.innerText + `1/(${currentValue})`;
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / parseFloat(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
}

function tenpow() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    if (internalmode == '1') {

        tillNowElement.innerText = tillNowElement.innerText + '2^' + currentValue;
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.pow(2,parseFloat(currentValue));
        element.innerText = currentValue;
    }
    else {
        tillNowElement.innerText = tillNowElement.innerText + '10^' + currentValue;
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.pow(10,parseFloat(currentValue));;
        element.innerText = currentValue;
    }
    functionval = true;
    state = 0;
}

function log() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (internalmode == '1') {
        if (tillNowElement.innerText == '') {
            IsinputWaitMethodStarting.push(true);
        }
        else {
            IsinputWaitMethodStarting.push(false);
        }
        inputWaitMethods.push(true);
        inputWaitMethodType.push(2);
        inputWaitMethodSolveable.push(0);
        element = document.getElementById("currentTextArea");
        currentValue = element.innerText;
        tillNowElement.innerText = tillNowElement.innerText + `${currentValue} log base`;
        inputWaitMethodFirstVal.push(currentValue);
        state = 0;
    }
    else {
        if (tillNowElement.innerText == '') {
            newresult = true;
        }
        var str = viewString();
        var fullString = tillNowElement.innerText;
        var stringArray = fullString.split('');
        stringArray.splice(stringArray.length - str.length, str.length);
        
        tillNowElement.innerText = stringArray.join('') + `log(${str})`;
        if (result[result.length - 1] == currentValue) {
            console.log("from log " + result[result.length - 1] + " " + currentValue);
            result.pop();
        }
        
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.log10(parseFloat(currentValue));
        element.innerText = currentValue;
    }
    functionval = true;
    state = 0;
}

function ln() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    if (internalmode == '1') {

        tillNowElement.innerText = tillNowElement.innerText + 'e^' + currentValue;
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.pow(Math.E, parseFloat(currentValue));
        element.innerText = currentValue;
    }
    else {
        tillNowElement.innerText = tillNowElement.innerText + 'ln(' + currentValue + ')';
        previousValue = currentValue;
        ValueIsChanged = true;
        currentValue = Math.log(parseFloat(currentValue));
        element.innerText = currentValue;
    }
    functionval = true;
    state = 0;
}

function exp() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    tillNowElement.innerText = tillNowElement.innerText + `exp(${currentValue})`;
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.exp(parseFloat(currentValue));
    element.innerText = currentValue;
    functionval = true;
    state = 0;
}

function sin() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length); 
    tillNowElement.innerText = stringArray.join('') + `sin(${str})`; 
    if (result[result.length - 1] == currentValue) {
        console.log("from sin " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200; 
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    
    currentValue = Math.sin(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0"; 

}

function cos() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `cos(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from cos " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.cos(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function tan() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `tan(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from tan " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.tan(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function sec() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `sec(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from sec " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / Math.cos(currentValue); 
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function cosec() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `cosec(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from sin " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / Math.sin(currentValue); 
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function cot() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `cot(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from cot " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / Math.tan(currentValue); 
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function asin() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `asin(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from sinInv " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.asin(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function acos() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `acos(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from acos " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.acos(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function atan() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `atan(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from tanInv " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.atan(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function acosec() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `acosec(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from cosecInv " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / Math.asin(currentValue); 
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function asec() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `asec(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from secInv " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / Math.acos(currentValue); 
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function acot() {
    unitElements = document.getElementById('units');
    unit = unitElements.value;
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `acot(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from cotInv " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    if (unit == '0') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 180;
    } else if (unit == '1') {
        previousValue = currentValue;
        currentValue = currentValue * Math.PI / 200;
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = 1 / Math.atan(currentValue); 
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("trigFunc");
    selectElement.value = "0";
}

function handleTrigFunction() {
    var selectElement = document.getElementById("trigFunc");
    var selectedValue = selectElement.value;
    switch (selectedValue) {
        case '1':
            sin();
            break;
        case '2':
            cos();
            break;
        case '3':
            tan();
            break;
        case '4':
            cosec();
            break;
        case '5':
            sec();
            break;
        case '6':
            cot();
            break;
        case '7':
            asin();
            break;
        case '8':
            acos();
            break;
        case '9':
            atan();
            break;
        case '10':
            acosec();
            break;
        case '11':
            asec();
            break;
        case '12':
            acot();
            break;
        default:
            // Eat FiveStar Do Nothing
            break;
    }
}

function floor() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `floor(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from floor " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.floor(parseFloat(currentValue));
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("randFunc");
    selectElement.value = "0";
}

function ciel() {
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    if (tillNowElement.innerText == '') {
        newresult = true;
    }
    var str = viewString();
    var fullString = tillNowElement.innerText;
    var stringArray = fullString.split('');
    stringArray.splice(stringArray.length - str.length, str.length);
    tillNowElement.innerText = stringArray.join('') + `ciel(${str})`;
    if (result[result.length - 1] == currentValue) {
        console.log("from ciel " + result[result.length - 1] + " " + currentValue);
        result.pop();
    }
    previousValue = currentValue;
    ValueIsChanged = true;
    currentValue = Math.ceil(currentValue);
    element.innerText = currentValue;
    functionval = true;
    state = 0;
    var selectElement = document.getElementById("randFunc");
    selectElement.value = "0";
}

function handelRandFunc() {
    var selectElement = document.getElementById("randFunc");
    var selectedValue = selectElement.value;
    switch (selectedValue) {
        case '1':
            floor();
            break;
        case '2':
            ciel();
            break;
        default:
            //Eat FiveStar Do Nothing
            break;
    }
}

function mod() {
    if (tillNowElement.innerText == '') {
        IsinputWaitMethodStarting.push(true);
    }
    else {
        IsinputWaitMethodStarting.push(false);
    }
    inputWaitMethods.push(true);
    inputWaitMethodType.push(3);
    inputWaitMethodSolveable.push(0);
    element = document.getElementById("currentTextArea");
    currentValue = element.innerText;
    tillNowElement.innerText = tillNowElement.innerText + `${currentValue}mod`;
    inputWaitMethodFirstVal.push(currentValue);
    state = 0;
}

function randpow() {
    console.log(internalmode);
    if (internalmode == '1') {
        if (tillNowElement.innerText == '') {
            IsinputWaitMethodStarting.push(true);
        }
        else {
            IsinputWaitMethodStarting.push(false);
        }
        inputWaitMethods.push(true);
        inputWaitMethodType.push(1);
        inputWaitMethodSolveable.push(0);
        element = document.getElementById("currentTextArea");
        currentValue = element.innerText;
        tillNowElement.innerText = tillNowElement.innerText + `${currentValue}^1/`;
        inputWaitMethodFirstVal.push(currentValue);
        state = 0;
    }
    else {
        if (tillNowElement.innerText == '') {
            IsinputWaitMethodStarting.push(true);
        }
        else {
            IsinputWaitMethodStarting.push(false);
        }
        inputWaitMethods.push(true);
        inputWaitMethodType.push(0);
        inputWaitMethodSolveable.push(0);
        element = document.getElementById("currentTextArea");
        currentValue = element.innerText;
        tillNowElement.innerText = tillNowElement.innerText + `${currentValue}^`;
        inputWaitMethodFirstVal.push(currentValue);
        state = 0;
    }
}

function numberKeys() {
    var numKeys = document.querySelectorAll('.num');
    numKeys.forEach(button => {
        button.addEventListener('click', () => {
            ValueIsChanged = true;
            element = document.getElementById("currentTextArea");
            currentValue = element.innerText;
            const key = button.id;
            if (state == '0') {
                element.innerText = `${key}`;
                state = element.innerText;
            }
            else {
                element.innerText = currentValue + `${key}`;
                state = element.innerText;
            }
        });
    });
}

function viewString() {
    var currentView = tillNowElement.innerText;
    var BracketFound = false;
    var desiredString = "";
    var noOfSubBrackets = -1;
    if (anyBracket && noOfOpenBrackets == 0) {
        for (var i = currentView.length - 1; i >= 0; i--) {
            console.log(currentView[i]);
            if (BracketFound) {
                if (currentView[i] == '+' || currentView[i] == '-' || currentView[i] == 'x' || currentView[i] == '/') {
                    break;
                }
                else {
                    desiredString = desiredString + currentView[i];
                }
            }
            else {
                desiredString = desiredString + currentView[i];
                if (currentView[i] == '(') {
                    noOfSubBrackets = noOfSubBrackets - 1;
                    if (noOfSubBrackets === 0) {
                        BracketFound = true;
                    }
                }
                else if (currentView[i] == ')') {
                    if (noOfSubBrackets === -1) {
                        noOfSubBrackets = 0 + 1;
                    }
                    else {
                        noOfSubBrackets = noOfSubBrackets + 1;
                    }
                }
                else {
                    continue;
                }
            }
        }
    }
    else {
        for (var i = currentView.length - 1; i >= 0; i--) {
            if (currentView[i] == '+' || currentView[i] == '-' || currentView[i] == 'x' || currentView[i] == '/') {
                break;
            }
            else {
                desiredString = desiredString + currentView[i];
            }
        }
    }
    var newDesiredString = "";
    for (var i = desiredString.length - 1; i >= 0; i--) {
        newDesiredString = newDesiredString + desiredString[i];
    }
    return newDesiredString;
}


document.getElementById("memory").addEventListener("click", showMemory);
document.getElementById("history").addEventListener("click", showHistory);

function showMemory() {
    if (document.getElementById("memory").style.display === 'none' || document.getElementById("memory").style.display === '') {
        document.getElementById("history").style.display = 'none';
        document.getElementById("memory").style.display = 'block';
    }
}

function showHistory() {
    if (document.getElementById("history").style.display === 'none' || document.getElementById("history").style.display === '') {
        document.getElementById("memory").style.display = 'none';
        document.getElementById("history").style.display = 'block';
    }
}
let stdElements = document.querySelectorAll(".std");
let sciElements = document.querySelectorAll(".sci");

function toggle(mode) {
    if (mode === "std") {
        stdElements.forEach(element => {
            element.style.display = "block";
        });
        sciElements.forEach(element => {
            element.style.display = "none";
        });
    } else if (mode === "sci") {
        stdElements.forEach(element => {
            element.style.display = "none";
        });
        sciElements.forEach(element => {
            element.style.display = "block";
        });
    }
    currentMode = mode;
}

document.getElementById("standard").addEventListener("click", stdcloseNav);
document.getElementById("scientific").addEventListener("click", scicloseNav);
document.getElementById("currentModeext").addEventListener("click", openNav);

function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function stdcloseNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("currentModeext").innerHTML = '<span style="font-size: 30px; cursor: pointer;" onclick="openNav()" id="currentModeext">&#9776; Standard</span>';
    toggle("std");
}
function scicloseNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("currentModeext").innerHTML = '<span style="font-size: 30px; cursor: pointer;" onclick="openNav()" id="currentModeext">&#9776; Scientific</span>';
    toggle("sci");
}














































/*if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '0' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '1' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '2' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '3' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '4' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '5' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '6' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '7' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '8' ||
                    tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '9') {
                    element.innerText = "0";
                    state = "0";
                    decimalIsSet = false;
                    tillNowElement.innerText = tillNowElement.innerText + 'x' + `${event.key}`;
                }
                else if (tillNowElement.innerText.substring(tillNowElement.innerText.length - 1, tillNowElement.innerText.length) == '.') {
                    element.innerText = "0";
                    state = "0";
                    decimalIsSet = false;
                    tillNowElement.innerText = tillNowElement.innerText + '0x' + `${event.key}`;
                }*/