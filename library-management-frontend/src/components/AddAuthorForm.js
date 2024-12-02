import React, { useState } from 'react';

const AddAuthorForm = ({ onAddAuthor }) => {
  const [formData, setFormData] = useState({
    name: '',
    surename: '',
    birthdate: '',
    country: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onAddAuthor(formData);
    setFormData({ name: '', surename: '', birthdate: '', country: '' });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="mb-3">
        <label className="form-label">Имя</label>
        <input type="text" className="form-control" name="name" value={formData.name} onChange={handleChange} required />
      </div>

      <div className="mb-3">
        <label className="form-label">Фамилия</label>
        <input type="text" className="form-control" name="surename" value={formData.surename} onChange={handleChange} required />
      </div>

      <div className="mb-3">
        <label className="form-label">Дата рождения</label>
        <input type="date" className="form-control" name="birthdate" value={formData.birthdate} onChange={handleChange} required />
      </div>

      <div className="mb-3">
        <label className="form-label">Страна</label>
        <input type="text" className="form-control" name="country" value={formData.country} onChange={handleChange} required />
      </div>

      <button type="submit" className="btn btn-primary">Добавить</button>
    </form>
  );
};

export default AddAuthorForm;
