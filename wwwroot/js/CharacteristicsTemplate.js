function DeleteCategory(id_btn) {
    //delete categry and change name and id other category and elements
    $(`[id^="[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}]"]`).each(function () {
        this.parentNode.removeChild(this);
    });
    $(`[id^="DelElement[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}]"]`).each(function () {
        this.parentNode.removeChild(this);
    });
    $(`[id^="DelCategory[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}]"]`).each(function () {
        this.parentNode.removeChild(this);
    });
    $(`[id^="CatDiv[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}]"]`).each(function () {
        this.parentNode.removeChild(this);
    });
    if (id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']')) < (countCategory - 1)) {
        for (let i = id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']')) - 1; i < countCategory - 1; i++) {
            console.log(i);
            console.log(id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']')));
            let countForDeleteSubCategory = 0;
            $(`[id^="[${i + 1}].["]`).each(function () {
                this.id = `[${i}].[${countForDeleteSubCategory}]`;
                this.name = `[${i}].Value[${countForDeleteSubCategory}].Name`;
                console.log(this.id);
                countForDeleteSubCategory++;
            });
            countForDeleteSubCategory = 0;
            $(`[id^="DelElement[${i + 1}]["]`).each(function () {
                this.id = `DelElement[${i}][${countForDeleteSubCategory}]`;
                countForDeleteSubCategory++;
            });
            countForDeleteSubCategory = 0;
            $(`[id^="CatDiv[${i + 1}]["]`).each(function () {
                this.id = `CatDiv[${i}][${countForDeleteSubCategory}]`;
                countForDeleteSubCategory++;
            });
            $(`[id="[${i + 1}]Cat"]`).each(function () {
                this.id = `[${i}]Cat`;
                this.name = `[${i}].Key`;
            });
            $(`[id="DelCategory[${i + 1}]"]`).each(function () {
                this.id = `DelCategory[${i}]`;
            });
            $(`[id="CatDiv[${i + 1}]"]`).each(function () {
                this.id = `CatDiv[${i}]`;
            });
        }
    }
    countCategory--;
}
function DeleteElement(id_btn) {
    //delete element and change name and id other elements
    let delBtn = document.getElementById(id_btn);
    let delInput = document.getElementById(`[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}].[${id_btn.slice(id_btn.lastIndexOf('[') + 1, id_btn.lastIndexOf(']'))}]`);
    let delDiv = document.getElementById(`CatDiv[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}][${id_btn.slice(id_btn.lastIndexOf('[') + 1, id_btn.lastIndexOf(']'))}]`);
    console.log(`[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}].[${id_btn.slice(id_btn.lastIndexOf('[') + 1, id_btn.lastIndexOf(']'))}]`);
    delInput.parentNode.removeChild(delInput);
    delBtn.parentNode.removeChild(delBtn);
    delDiv.parentNode.removeChild(delDiv);
    let countForDeleteSubCategory = 0;
    $(`[id^="[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}].["]`).each(function () {
        this.id = `[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}].[${countForDeleteSubCategory}]`;
        this.name = `[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}].Value[${countForDeleteSubCategory}].Name`;
        countForDeleteSubCategory++;
    });
    if (id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']')) == (countCategory - 1)) {
        countElementCharact = countForDeleteSubCategory;
    }
    countForDeleteSubCategory = 0;
    $(`[id^="DelElement[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}]["]`).each(function () {
        this.id = `DelElement[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}][${countForDeleteSubCategory}]`;
        countForDeleteSubCategory++;
    });
    countForDeleteSubCategory = 0;
    $(`[id^="CatDiv[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}]["]`).each(function () {
        this.id = `CatDiv[${id_btn.slice(id_btn.indexOf('[') + 1, id_btn.indexOf(']'))}][${countForDeleteSubCategory}]`;
        countForDeleteSubCategory++;
    });
}
function DeleteErrorCategory() {
    if (document.getElementById("errCategory")) {
        let err = document.getElementById("errCategory");
        err.parentNode.removeChild(err);
    }
}
function ExistCategory() {
    let existCategry = false;
    $("[id$='Cat']").each(function () {
        existCategry = true;
    });
    return existCategry;
}
function CheckEmptyCategory() {
    if (countCategory > 0) {
        for (let i = 0; i < countCategory; i++) {
            if (document.getElementById(`[${i}]Cat`)) {
                let checkCat = document.getElementById(`[${i}]Cat`).value;
                if (checkCat != "") checkEmpty = true;
                else {
                    checkEmpty = false;
                    break;
                }
            }
        }
    }
    if (!ExistCategory()) {
        checkEmpty = true;
    }
}
function CreateCategoryCharacteristic() {
    //create category, clear errors if they exist, check if previous category value empty
    DeleteErrorCategory();
    CheckEmptyCategory();
    if (checkEmpty) {
        let categoryCharc = document.createElement("input");
        let categoryDiv = document.createElement("div");
        let categoryBtn = document.createElement("input");
        categoryBtn.setAttribute("id", `DelCategory[${countCategory}]`);
        categoryBtn.setAttribute("type", "button");
        categoryBtn.setAttribute("value", "Delete");
        categoryBtn.setAttribute("onclick", "DeleteCategory(this.id)");
        categoryBtn.setAttribute("class", "btn btn-danger");
        categoryDiv.setAttribute("class", "form-inline");
        categoryDiv.setAttribute("id", `CatDiv[${countCategory}]`);
        categoryCharc.setAttribute("type", "text");
        categoryCharc.setAttribute("class", "form-control");
        categoryCharc.setAttribute("placeholder", "Category");
        categoryCharc.setAttribute("id", `[${countCategory}]Cat`);
        categoryCharc.setAttribute("name", `[${countCategory}].Key`);
        document.getElementById("Сharacteristic").appendChild(categoryDiv);
        document.getElementById(`CatDiv[${countCategory}]`).appendChild(categoryCharc);
        document.getElementById(`CatDiv[${countCategory}]`).appendChild(categoryBtn);
        countCategory++;
        countElementCharact = 0;
    }
    else {
        //if previous category value empty, then show error
        var errCreate = document.createElement("p");
        errCreate.setAttribute("id", "errCategory");
        errCreate.innerHTML = "Fields cannot be empty";
        errCreate.style.color = "red";
        document.getElementById("Сharacteristic").appendChild(errCreate);
    }
}
function CreateSubcategorySettings(catDesc) {
    //create element category, and clear if exist errors
    DeleteErrorCategory();
    if (!ExistCategory()) {
        CreateCategoryCharacteristic();
    }
    var categoryElement = document.createElement("input");
    let categoryDiv = document.createElement("div");
    let categoryBtn = document.createElement("input");
    categoryBtn.setAttribute("id", `DelElement[${countCategory - 1}][${countElementCharact}]`);
    categoryBtn.setAttribute("type", "button");
    categoryBtn.setAttribute("value", "Delete");
    categoryBtn.setAttribute("onclick", "DeleteElement(this.id)");
    categoryBtn.setAttribute("class", "btn btn-danger");
    categoryDiv.setAttribute("class", "form-inline");
    categoryDiv.setAttribute("id", `CatDiv[${countCategory - 1}][${countElementCharact}]`);
    categoryElement.style.marginLeft = "30px";
    categoryElement.setAttribute("type", "text");
    categoryElement.setAttribute("class", "form-control");
    categoryElement.setAttribute("placeholder", "Element category");
    categoryElement.setAttribute("id", `[${countCategory - 1}].[${countElementCharact}]`);
    categoryElement.setAttribute("name", `[${countCategory - 1}].Value[${countElementCharact}].Name`);
    document.getElementById("Сharacteristic").appendChild(categoryDiv);
    document.getElementById(`CatDiv[${countCategory - 1}][${countElementCharact}]`).appendChild(categoryElement);
    if (catDesc) {
        let categoryDescription = document.createElement("input");
        categoryDescription.setAttribute("type", "text");
        categoryDescription.setAttribute("class", "form-control");
        categoryDescription.setAttribute("placeholder", "Description");
        categoryDescription.setAttribute("id", `[${countCategory - 1}].[${countElementCharact}]`);
        categoryDescription.setAttribute("name", `[${countCategory - 1}].Value[${countElementCharact}].Description`);
        document.getElementById(`CatDiv[${countCategory - 1}][${countElementCharact}]`).appendChild(categoryDescription);
    }
    document.getElementById(`CatDiv[${countCategory - 1}][${countElementCharact}]`).appendChild(categoryBtn);
    countElementCharact++;
}