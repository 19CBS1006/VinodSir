<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Sci_Calculator_web_Form.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sci Calculator</title>
    
    <link rel="stylesheet" href="/Styles/StyleSheet1.css" />
</head>
<body>
    <div id="main">
        <div id="left">
            <div id="display">
                <div id="mySidenav" class="sidenav">
                  <a id="standard" >Standard</a>
                  <a id="scientific" >Scientific</a>
                </div>
                <span style="font-size:30px;cursor:pointer" id="currentModeext" >&#9776; Scientific</span>
                <div id="tillnow">
                    <textbox id="tillnowtextArea"></textbox>
                </div>
                <div id="current">
                    <textbox id="currentTextArea">0</textbox>
                </div>
            </div>
            <div id="leftbottom">
                <div id="status" class="sci">
                    <button>F-E</button><!--
                --><select id="units" style=" margin:0;width:10%; height:50px; background-color: #2c2d2d; color: white; border-radius: 5px; border: none; margin: 5px 5px; text-align:center;">
                       <option value="0">Deg</option>
                       <option value="1">Grad</option>
                       <option value="2">Rad</option>
                     </select>
                </div>
                <div id="memoryopr">
                   <button onclick="memoryClear()">MC</button><!--
                --><button onclick="memoryRecall()">MR</button><!--
                --><button onclick="memoryStore()">MS</button><!--
                --><button onclick="memoryPlus()">M+</button><!--
                --><button onclick="memoryMinus()">M-</button>
                   <select id="trigFunc" style=" margin:0;width:15%; height:50px; background-color: #2c2d2d; color: white; border-radius: 5px; border: none; margin: 5px 5px; text-align:center;" onchange="handleTrigFunction()">
                      <option value="0">Trignometry</option>
                      <option value="1">sin</option>
                      <option value="2">cos</option>
                      <option value="3">tan</option>
                      <option value="4">cosec</option>
                      <option value="5">sec</option>
                      <option value="6">cot</option>
                      <option value="7">sin Inverse</option>
                      <option value="8">cos Inverse</option>
                      <option value="9">tan Inverse</option>
                      <option value="10">cosec Inverse</option>
                      <option value="11">sec Inverse</option>
                      <option value="12">Cot Inverse</option>
                    </select>
                    <select id="randFunc" style=" margin:0;width:15%; height:50px; background-color: #2c2d2d; color: white; border-radius: 5px; border: none; margin: 5px 5px; text-align:center;" onchange="handelRandFunc()">
                       <option value="0">Function</option>
                       <option value="1">floor</option>
                       <option value="2">Ceil</option>
                     </select>
                </div>
                <div id="keypad1" class="std" style="display:none">
                    <div>
                        <button>%</button><!--
                     --><button>CE</button><!--
                     --><button>C</button><!--
                     --><button>BackSpace</button>
                    </div>
                    <div>
                        <button>1/x</button><!--
                     --><button>x<sup>2</sup></button><!--
                     --><button>sqrt</button><!--
                     --><button>/</button>
                    </div>
                    <div>
                        <button class="num">7</button><!--
                     --><button class="num">8</button><!--
                     --><button class="num">9</button><!--
                     --><button>X</button>
                    </div>
                    <div>
                        <button class="num">4</button><!--
                     --><button class="num">5</button><!--
                     --><button class="num">6</button><!--
                     --><button>-</button>
                    </div>
                    <div>
                        <button class="num">1</button><!--
                     --><button class="num">2</button><!--
                     --><button class="num">3</button><!--
                     --><button>+</button>
                    </div>
                    <div>
                        <button class="num"><sup>+</sup>/<sub>-</sub></button><!--
                     --><button class="num">0</button><!--
                     --><button class="num">.</button><!--
                     --><button id="stdequal">=</button>
                    </div>
                </div>
                <div id="keypad2" class="sci">
                    <div>
                        <button onclick="secondTogggler()">2<sup>nd</sup></button><!--
                     --><button onclick="pi()">π</button><!--
                     --><button onclick="e()">e</button><!--
                     --><button id="CCE" onclick="ce()">CE</button><!--
                     --><button onclick="backspace()">Backspace</button>
                    </div>
                    <div>
                        <button onclick="xsq()">x<sup>2</sup></button><!--
                     --><button onclick="inverse()">1/x</button><!--
                     --><button onclick="abs()">|x|</button><!--
                     --><button onclick="exp()">exp</button><!--
                     --><button onclick="mod()">mod</button>
                    </div>
                    <div>
                        <button onclick="rt()">sqrt</button><!--
                     --><button>(</button><!--
                     --><button>)</button><!--
                     --><button onclick="fact()">n!</button><!--
                     --><button>÷</button>
                    </div>
                    <div>
                        <button onclick="randpow()">x<sup>y</sup></button><!--
                     --><button class="num">7</button><!--
                     --><button class="num">8</button><!--
                     --><button class="num">9</button><!--
                     --><button>x</button>
                    </div>
                    <div>
                        <button onclick="tenpow()">10<sup>x</sup></button><!--
                     --><button class="num">4</button><!--
                     --><button class="num">5</button><!--
                     --><button class="num">6</button><!--
                     --><button>-</button>
                    </div>
                    <div>
                        <button onclick="log()">log</button><!--
                     --><button class="num">1</button><!--
                     --><button class="num">2</button><!--
                     --><button class="num">3</button><!--
                     --><button>+</button>
                    </div>
                    <div>
                        <button onclick="ln()">ln</button><!--
                     --><button onclick="negate()"><sup>+</sup>/<sub>-</sub></button><!--
                     --><button class="num">0</button><!--
                     --><button >.</button><!--
                     --><button id="sciequal" onclick="">=</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="right">
            <div id="rightbtns">
                <button onclick="showHistory()">history</button><!--
             --><button onclick="showMemory()">memory</button>
            </div>
            <div id="history">
                <p id="historyPlaceBefore">History content</p>
            </div>
            <div id="memory" style="display: none;">
                <p id="memoryPlaceBefore">Memory content</p>
            </div>
        </div>
    </div>
    <script src="/MyJS/JavaScript.js"></script> 
</body>
</html>
