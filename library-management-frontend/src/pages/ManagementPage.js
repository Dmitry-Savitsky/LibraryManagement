import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { addBookCharacteristic } from '../http/bookCharacteristicsApi';
import { addAuthor, deleteAuthor, getAllAuthors } from '../http/authorApi'; 
import AddAuthorForm from '../components/AddAuthorForm';

const ManagementPage = () => {
  const [authors, setAuthors] = useState([]);
  const [formData, setFormData] = useState({
    ISBN: '',
    Title: '',
    Genre: '',
    Description: '',
    AuthorId: '',
    CheckoutPeriod: '',
    BookCount: '',
    Image: null,
  });
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const fetchAuthors = async () => {
    try {
      const data = await getAllAuthors();
      setAuthors(data);
    } catch (error) {
      setError('Error fetching authors.');
    }
  };

  const handleAddAuthor = async (authorData) => {
    try {
      const newAuthor = await addAuthor(authorData);
      setAuthors([...authors, newAuthor]);
    } catch (error) {
      setError('Error adding author.');
    }
  };

  const handleDeleteAuthor = async (id) => {
    try {
      await deleteAuthor(id);
      setAuthors(authors.filter((author) => author.id !== id));
    } catch (error) {
      setError('Error deleting author.');
    }
  };

  useEffect(() => {
    fetchAuthors();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleFileChange = (e) => {
    setFormData((prevState) => ({
      ...prevState,
      Image: e.target.files[0],
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!formData.ISBN || !formData.Title || !formData.Genre || !formData.Description || !formData.AuthorId || !formData.CheckoutPeriod || !formData.BookCount || !formData.Image) {
      setError('Please fill in all fields.');
      return;
    }

    const bookData = new FormData();
    bookData.append('ISBN', formData.ISBN);
    bookData.append('Title', formData.Title);
    bookData.append('Genre', formData.Genre);
    bookData.append('Description', formData.Description);
    bookData.append('AuthorId', formData.AuthorId);
    bookData.append('CheckoutPeriod', formData.CheckoutPeriod);
    bookData.append('BookCount', formData.BookCount);
    bookData.append('Image', formData.Image);

    try {
      await addBookCharacteristic(bookData);
      navigate(`/books`);
    } catch (error) {
      setError('Error adding book characteristic.');
    }
  };

  return (
    <div className="container mt-5">
      {error && <div className="alert alert-danger">{error}</div>}

      <div className="row">
        <div className="col-md-6">
          <h3>Список авторов</h3>
          <ul className="list-group mb-4">
            {authors.map((author) => (
              <li key={author.id} className="list-group-item d-flex justify-content-between align-items-center">
                {`${author.name} ${author.surename}`}
                <button className="btn btn-danger btn-sm" onClick={() => handleDeleteAuthor(author.id)}>
                  Удалить
                </button>
              </li>
            ))}
          </ul>
          <h4>Добавить автора</h4>
          <AddAuthorForm onAddAuthor={handleAddAuthor} />
        </div>

        <div className="col-md-6">
          <h3>Добавить книгу</h3>
          <form onSubmit={handleSubmit} className="card p-4 shadow-sm">
            {['ISBN', 'Title', 'Genre', 'Description', 'AuthorId', 'CheckoutPeriod', 'BookCount'].map((field) => (
              <div className="mb-3" key={field}>
                <label htmlFor={field} className="form-label">
                  {field}
                </label>
                <input
                  type={field === 'Description' ? 'textarea' : 'text'}
                  className="form-control"
                  id={field}
                  name={field}
                  value={formData[field]}
                  onChange={handleInputChange}
                  required
                />
              </div>
            ))}

            <div className="mb-3">
              <label htmlFor="Image" className="form-label">Book image</label>
              <input
                type="file"
                className="form-control"
                id="Image"
                name="Image"
                onChange={handleFileChange}
                required
              />
            </div>

            <button type="submit" className="btn btn-primary w-100">Добавить книгу</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ManagementPage;
