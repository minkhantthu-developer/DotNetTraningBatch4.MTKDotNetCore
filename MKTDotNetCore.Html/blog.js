const tblBlog = 'blogs';
let blogId = null;

getBlogTable();

function readBlog() {
    let lst = getBlogs();
}

function editBlog(id) {
    let lst = getBlogs();
    const items = lst.filter(x => x.id === id);
    console.log(items);
    console.log(items.length);
    if (items.length === 0) {
        console.log("no data found");
        return;
    }
    let item = items[0];
    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();

}

function createBlog(title, author, content) {
    let lst = getBlogs();
    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    }
    lst.push(requestModel);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);
    successMessage("Successfully Save");
    clearControl();
    getBlogTable();

}

function updateBlog(id, title, author, content) {
    let lst = getBlogs();
    const items = lst.filter(x => x.id === id);
    console.log(items);
    console.log(items.length);
    if (items.length === 0) {
        console.log("no data found");
        return;
    }
    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;
    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);
    successMessage("Successfully Update");
    getBlogTable();
}

function deleteBlog(id) {

    confirmMessage("Are you sure want to delete").then(
        function(value){
            let lst = getBlogs();
            const items = lst.filter(x => x.id === id);
            if (items.length == 0) {
                console.log('No data found');
                return;
            }
            lst = lst.filter(x => x.id != id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);
            successMessage("Successfully Delete");
            getBlogTable();
        }
    );
}

function deleteBlog1(id) {

    Notiflix.Confirm.show(
        'Notiflix Confirm',
        'Do you agree with me?',
        'Yes',
        'No',
        function okCb() {
            let lst = getBlogs();
            const items = lst.filter(x => x.id === id);
            if (items.length == 0) {
                console.log('No data found');
                return;
            }
            lst = lst.filter(x => x.id != id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);
            successMessage("Successfully Delete");
            getBlogTable();
        },

        {
        },
    );
}

function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    if (blogId === null) {
        createBlog(title, author, content);
    }
    else {
        updateBlog(blogId, title, author, content);
        blogId = null;
        clearControl();
    }
})

function clearControl() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td scope="row">${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
        `;
        htmlRows += htmlRow;
    });
    $('#tbody').html(htmlRows);
}