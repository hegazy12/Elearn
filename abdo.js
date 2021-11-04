function muphone(){
    var x = document.getElementById("myList");
    if (x.style.display === "block") {
      x.style.display = "none";
    } else if(x.style.display === "none"){
      x.style.display = "block";
    }else if(x.style.display == ""){
        x.style.display = "none";
    }
  }

var background=new Array();
background.push("1.jpg");
background.push("2.jpg");
background.push("3.jpg");
background.push("4.jpg");
background.push("5.jpg");
background.push("6.jpg");
background.push("7.jpg");
background.push("8.jpg");
background.push("9.jpg");

if(screen.width <= 480){
  document.getElementById("logo").addEventListener("click", muphone)
};