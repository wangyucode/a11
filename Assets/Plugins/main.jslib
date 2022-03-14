mergeInto(LibraryManager.library, {
    ShowLink: function (str) {
        var link = UTF8ToString(str);
        console.log(link);

        var container = document.querySelector("#images-container");
        var landingPage = document.querySelector("#landing-page")
        landingPage.style.display = 'block';

        var appendElement = function (data) {
            data.forEach(function (e) {
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
        }

        switch (link) {
            case 'text1':
                appendElement(window.landingData.text1);
                break;
            case 'text2':
                appendElement(window.landingData.text2);
                break;
            case 'text3':
                appendElement(window.landingData.text3);
                break;
            case 'text4':
                appendElement(window.landingData.text4);
                break;
            case 'text5':
                appendElement(window.landingData.text5);
                break;
        }
    },

});