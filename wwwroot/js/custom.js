let loadMore = document.querySelector('.btn-load-more');

loadMore.addEventListener('click', function () {
    let loadedCount = document.querySelectorAll(".load-products .product").length;
    let totalCount = parseInt(
        document.querySelector(".load-products .product-count").value
    );

    fetch(`/Home/LoadMore?skip=${loadedCount}`)
        .then(res => res.text())
        .then(data => {

            if (data.trim() === "") {
                this.style.display = 'none';
                return;
            }

            document.querySelector('.load-products')
                .insertAdjacentHTML("beforeend", data);

            loadedCount = document.querySelectorAll(".load-products .product").length;

            if (loadedCount >= totalCount) {
                this.style.display = 'none';
            }
        })
        .catch(err => console.error(err));
});

document.addEventListener("DOMContentLoaded", function () {
    
    let buttons = document.querySelectorAll(".add-basket-btn");

    buttons.forEach(btn => {
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            let productId = this.getAttribute("data-id");

            fetch("/Basket/Add?id=" + productId, {
                method: "POST",
            })
            .then(res => {
                if (res.ok) {
                    return res.json();
                } else {
                    alert("Error!");
                }
            })
            .then(data => {
                if (data) {
                    alert(data.message);

                    let basketCountEl = document.getElementById("basket-count");
                    if (basketCountEl) {
                        basketCountEl.innerText = data.count;
                    }
                    let basketPriceEl = document.getElementById("basket-total-price");
                    if (basketPriceEl) {
                        basketPriceEl.innerText = data.totalPrice;
                    }
                }
            })
            .catch(err => console.error("Error:", err));
        });
    });
});