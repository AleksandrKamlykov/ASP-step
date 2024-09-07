document.addEventListener('DOMContentLoaded', function () {
    
    const form = document.getElementById('booksForm');
    
    form.addEventListener('submit', async function (event) {
        event.preventDefault();
        const category = document.getElementById('category').value;
        const books = await getBooks(category);
        const b = document.querySelector('tbody');
        b.innerHTML = '';
        
        books.forEach((book, index) => {
            const row = document.createElement('tr');
            row.innerHTML = `
               <tr>
                <th scope="row">${index +1}</th>
                <td>${book.title}</td>
                <td>${book.category}</td>
                <td>${book.author}</td>
            </tr>
            `;
            b.appendChild(row);
        });
 
    }

    );
    
    
});


async function getBooks(category) {
    
    const url = new URL('http://localhost:5128/api/books');
    if(category) {
        url.searchParams.append('category', category);
    }
    const response = await fetch(url.href);
    const data = await response.json();
    return data;
}