import React, { useState, useEffect } from 'react';
import { updateBookCharacteristic } from '../http/bookCharacteristicsApi';
const EditBookForm = ({
  bookId,
  currentIsbn,
  currentTitle,
  currentGenre,
  currentDescription,
  currentCheckoutPeriod,
  currentBookCount,
  onClose,
}) => {
  const [formData, setFormData] = useState({
    ISBN: currentIsbn,
    Title: currentTitle,
    Genre: currentGenre,
    Description: currentDescription,
    CheckoutPeriod: currentCheckoutPeriod,
    BookCount: currentBookCount,
    Image: null,
  });
  const [error, setError] = useState('');

  useEffect(() => {
    setFormData({
      ISBN: currentIsbn,
      Title: currentTitle,
      Genre: currentGenre,
      Description: currentDescription,
      CheckoutPeriod: currentCheckoutPeriod,
      BookCount: currentBookCount,
      Image: null,
    });
  }, [currentIsbn, currentTitle, currentGenre, currentDescription, currentCheckoutPeriod, currentBookCount]);

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

    if (!formData.ISBN || !formData.Title || !formData.Genre || !formData.Description || !formData.CheckoutPeriod || !formData.BookCount) {
      setError('Please fill in all fields.');
      return;
    }

    const updatedData = new FormData();
    updatedData.append('ISBN', formData.ISBN);
    updatedData.append('Title', formData.Title);
    updatedData.append('Genre', formData.Genre);
    updatedData.append('Description', formData.Description);
    updatedData.append('CheckoutPeriod', formData.CheckoutPeriod);
    updatedData.append('BookCount', formData.BookCount);
    if (formData.Image) {
      updatedData.append('Image', formData.Image);
    }

    try {
      await updateBookCharacteristic(bookId, updatedData); onClose();
    } catch (error) {
      setError('Error updating book characteristic.');
    }
  };

  return (
    <div>
      <h2>Edit Book</h2>
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
          <label htmlFor="Image" className="form-label">Book Image (optional)</label>
          <input
            type="file"
            className="form-control"
            id="Image"
            name="Image"
            onChange={handleFileChange}
          />
        </div>

        <button type="submit" className="btn btn-primary">Update Book</button>
      </form>
    </div>
  );
};

export default EditBookForm;
