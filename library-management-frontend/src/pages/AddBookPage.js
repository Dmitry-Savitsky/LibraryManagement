import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { addBookCharacteristic } from '../http/bookCharacteristicsApi';

const AddBookPage = () => {
  const navigate = useNavigate();
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
      const data = await addBookCharacteristic(bookData);
      navigate(`/books`);
    } catch (error) {
      setError('Error adding book characteristic.');
    }
  };

  return (
    <div className="container mt-5">
      <h2>Add New Book</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="ISBN" className="form-label">ISBN</label>
          <input
            type="text"
            className="form-control"
            id="ISBN"
            name="ISBN"
            value={formData.ISBN}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="Title" className="form-label">Title</label>
          <input
            type="text"
            className="form-control"
            id="Title"
            name="Title"
            value={formData.Title}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="Genre" className="form-label">Genre</label>
          <input
            type="text"
            className="form-control"
            id="Genre"
            name="Genre"
            value={formData.Genre}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="Description" className="form-label">Description</label>
          <textarea
            className="form-control"
            id="Description"
            name="Description"
            value={formData.Description}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="AuthorId" className="form-label">Author ID</label>
          <input
            type="number"
            className="form-control"
            id="AuthorId"
            name="AuthorId"
            value={formData.AuthorId}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="CheckoutPeriod" className="form-label">Checkout Period (days)</label>
          <input
            type="number"
            className="form-control"
            id="CheckoutPeriod"
            name="CheckoutPeriod"
            value={formData.CheckoutPeriod}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="BookCount" className="form-label">Book Count</label>
          <input
            type="number"
            className="form-control"
            id="BookCount"
            name="BookCount"
            value={formData.BookCount}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="Image" className="form-label">Book Image</label>
          <input
            type="file"
            className="form-control"
            id="Image"
            name="Image"
            onChange={handleFileChange}
            required
          />
        </div>

        <button type="submit" className="btn btn-primary">Add Book</button>
      </form>
    </div>
  );
};

export default AddBookPage;
