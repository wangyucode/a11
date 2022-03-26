var canvas = document.querySelector("#unity-canvas");
var loading = document.querySelector("#index-loading");
var returnBtn = document.querySelector("#return-btn");
var imageContainer = document.querySelector("#images-container");
var landingPage = document.querySelector("#landing-page")
var sound = document.querySelector("#sound")

window.landingData = {
    pepper: [
        { tag: 'img', url: 'landing/pepper/1.jpg' },
        { tag: 'img', url: 'landing/pepper/2.jpg' },
        { tag: 'img', url: 'landing/pepper/3.jpg' },
        { tag: 'img', url: 'landing/pepper/4.jpg' },
        { tag: 'img', url: 'landing/pepper/5.jpg' },
        { tag: 'img', url: 'landing/pepper/6.jpg' },
        { tag: 'img', url: 'landing/pepper/7.jpg' },
    ],
    vial: [
        { tag: 'img', url: 'landing/vial/1.jpg' },
        { tag: 'img', url: 'landing/vial/2.jpg' },
        { tag: 'img', url: 'landing/vial/3.jpg' },
        { tag: 'img', url: 'landing/vial/4.jpg' },
        { tag: 'img', url: 'landing/vial/5.jpg' },
        { tag: 'img', url: 'landing/vial/6.jpg' },
        { tag: 'img', url: 'landing/vial/7.jpg' },
        { tag: 'img', url: 'landing/vial/8.jpg' },
        { tag: 'img', url: 'landing/vial/9.jpg' },
    ],
    EX: [
        { tag: 'img', url: 'landing/ex/1.jpg' },
        { tag: 'img', url: 'landing/ex/2.jpg' },
        { tag: 'img', url: 'landing/ex/3.jpg' },
        { tag: 'img', url: 'landing/ex/4.jpg' },
        { tag: 'img', url: 'landing/ex/5.jpg' },
        { tag: 'img', url: 'landing/ex/6.jpg' },
        { tag: 'img', url: 'landing/ex/7.jpg' },
        { tag: 'img', url: 'landing/ex/8.jpg' },
        { tag: 'img', url: 'landing/ex/9.jpg' },
        { tag: 'img', url: 'landing/ex/10.jpg' },
        { tag: 'img', url: 'landing/ex/11.jpg' },
    ],
    TT: [
        { tag: 'img', url: 'landing/tt/1.jpg' },
        { tag: 'img', url: 'landing/tt/2.jpg' },
        { tag: 'img', url: 'landing/tt/3.jpg' },
        { tag: 'img', url: 'landing/tt/4.jpg' },
        { tag: 'img', url: 'landing/tt/5.jpg' },
        { tag: 'img', url: 'landing/tt/6.jpg' },
    ],
    jin: [
        { tag: 'img', url: 'landing/jin/1.jpg' },
        { tag: 'img', url: 'landing/jin/2.jpg' },
        { tag: 'img', url: 'landing/jin/3.jpg' },
        { tag: 'img', url: 'landing/jin/4.jpg' },
        { tag: 'img', url: 'landing/jin/5.jpg' },
        { tag: 'img', url: 'landing/jin/6.jpg' },
        { tag: 'img', url: 'landing/jin/7.jpg' },
        { tag: 'img', url: 'landing/jin/8.jpg' },
        { tag: 'img', url: 'landing/jin/9.jpg' },
    ],
    a11: [
        { tag: 'img', url: 'landing/a11/1.jpg' },
        { tag: 'img', url: 'landing/a11/2.jpg' },
        { tag: 'img', url: 'landing/a11/3.jpg' },
    ]
}

returnBtn.addEventListener('click', function () {
    landingPage.style.display = 'none';
    while (imageContainer.lastElementChild) {
        imageContainer.removeChild(imageContainer.lastElementChild);
    }
});

var playSound = function () {
    sound.load();
    window.removeEventListener('touchend', playSound)
}

sound.addEventListener('canplay', function () {
    sound.play();
});

window.addEventListener('touchend', playSound);

var buildUrl = "Build";
var loaderUrl = buildUrl + "/dist.loader.js";
var config = {
    dataUrl: buildUrl + "/dist.data.gz",
    frameworkUrl: buildUrl + "/dist.framework.js.gz",
    codeUrl: buildUrl + "/dist.wasm.gz",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "wycode",
    productName: "a11",
    productVersion: "0.1"
};

// By default Unity keeps WebGL canvas render target size matched with
// the DOM size of the canvas element (scaled by window.devicePixelRatio)
// Set this to false if you want to decouple this synchronization from
// happening inside the engine, and you would instead like to size up
// the canvas DOM size and WebGL render target sizes yourself.
// config.matchWebGLToCanvasSize = false;

//   if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
//     // Mobile device style: fill the whole browser client area with the game canvas:

//     var meta = document.createElement('meta');
//     meta.name = 'viewport';
//     meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
//     document.getElementsByTagName('head')[0].appendChild(meta);
//     container.className = "unity-mobile";

//     // To lower canvas resolution on mobile devices to gain some
//     // performance, uncomment the following line:
//     // config.devicePixelRatio = 1;

//     canvas.style.width = window.innerWidth + 'px';
//     canvas.style.height = window.innerHeight + 'px';

//     unityShowBanner('WebGL builds are not supported on mobile devices.');
//   } else {
//     // Desktop style: Render the game canvas in a window that can be maximized to fullscreen:

//     canvas.style.width = "960px";
//     canvas.style.height = "600px";
//   }

var script = document.createElement("script");
script.src = loaderUrl;
script.onload = () => {
    createUnityInstance(canvas, config, (progress) => {
        //   progressBarFull.style.width = 100 * progress + "%";
        console.log(progress);
    }).then((unityInstance) => {
        setTimeout(() => {
            loading.style.display = "none";
        }, 2000);
    }).catch((message) => {
        alert(message);
    });
}
document.body.appendChild(script);

function setWx(data) {
    const options = {
        debug: false,
        jsApiList: ['updateAppMessageShareData', 'updateTimelineShareData'],
        ...data
    };
    wx.config(options);

    wx.ready(function () {
        const shareData = {
            title: '未来基因 Recasting..',
            desc: '破解娱乐公式，释放束缚的DNA，在未来青年 Center A11 in!',
            link: location.href,
            imgUrl: `${location.href}/logo.jpg`
        }
        wx.updateAppMessageShareData(shareData);
        wx.updateTimelineShareData(shareData);
    });

}

fetch(`https://wycode.cn/node/a11/sign?url=${location.href}`)
    .then(response => response.json())
    .then(data => data && data.success && setWx(data.payload));
