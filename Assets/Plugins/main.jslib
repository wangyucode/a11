mergeInto(LibraryManager.library, {
    ShowLink: function (str) {
        var link = UTF8ToString(str);
        console.log(link);

        var container = document.querySelector("#images-container");
        var landingPage = document.querySelector("#landing-page")
        landingPage.style.display = 'block';

        // switch (link) {
        //     case 'Cube1':
                window.landingData.cube.forEach(function (e) {
                    var element = document.createElement(e.tag);
                    switch (e.tag) {
                        case 'img':
                            element.src = e.url;
                            break;
                        case 'a':
                            element.href = e.url;
                            element.target = '_blank';
                            element.innerHTML = e.text;
                            break;
                    }
                    container.appendChild(element);
                });
        //         break;
        // }
    },

});