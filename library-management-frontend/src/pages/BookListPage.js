import React, { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { Container, Row, Col, Card, Button, Form, Carousel } from "react-bootstrap";
import { getAllBookCharacteristics } from "../http/bookCharacteristicsApi";
import { updateBookCharacteristic, deleteBookCharacteristic } from "../http/bookCharacteristicsApi";   import { BOOK_DETAILS_ROUTE } from "../utils/consts";
import { Context } from "..";  
const BookListPage = () => {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [loading, setLoading] = useState(true);

  const navigate = useNavigate();
  const { user } = useContext(Context);  
  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const data = await getAllBookCharacteristics();
        setBooks(data);
        setFilteredBooks(data);
      } catch (error) {
        console.error("Error fetching books:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchBooks();
  }, []);

     useEffect(() => {
    const filtered = books.filter((book) =>
      book.title.toLowerCase().includes(searchQuery.toLowerCase())
    );
    setFilteredBooks(filtered);
  }, [searchQuery, books]);

  const groupBooksByGenre = (books) => {
    return books.reduce((acc, book) => {
      const genre = book.genre || "Без жанра";
      if (!acc[genre]) acc[genre] = [];
      acc[genre].push(book);
      return acc;
    }, {});
  };

  const booksByGenre = groupBooksByGenre(filteredBooks);

  const handleDeleteBook = async (bookId) => {
    try {
      await deleteBookCharacteristic(bookId);
      setBooks(books.filter((book) => book.id !== bookId));        alert("Книга удалена!");
    } catch (error) {
      alert("Ошибка при удалении книги!");
    }
  };

  const handleUpdateBook = (book) => {
    navigate(`/update-book/${book.id}`);    };

  if (loading) {
    return <div className="text-center mt-5">Loading...</div>;
  }

  return (
    <Container className="mt-5">
      <h2 className="mb-4">Список книг</h2>
      <Form.Control
        type="text"
        placeholder="Поиск по названию книги"
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        className="mb-4"
      />
      {Object.keys(booksByGenre).map((genre) => (
        <Card key={genre} className="mb-5 p-4" style={{ border: "2px solid #ccc", borderRadius: "8px" }}>
          <Card.Header as="h3" className="text-center mb-4">
            {genre}
          </Card.Header>
          <Row>
            {booksByGenre[genre].slice(0, 3).map((book) => (
              <Col key={book.id} sm={12} md={4} lg={4} className="mb-4">
                <Card>
                  <Card.Img
                    variant="top"
                    src={process.env.REACT_APP_API_URL + book.imgPath}
                    style={{ height: "200px", objectFit: "cover" }}
                  />
                  <Card.Body>
                    <Card.Title>{book.title}</Card.Title>
                    <Card.Text>
                      {book.bookCount > 0 ? (
                        <>
                          <strong>Доступно:</strong> {book.bookCount} шт.
                        </>
                      ) : (
                        <span className="text-danger">Нет в наличии</span>
                      )}
                    </Card.Text>
                    {user.isAuth ? (
                      <>
                        <Button
                          variant="primary"
                          disabled={book.bookCount === 0}
                          onClick={() => navigate(BOOK_DETAILS_ROUTE.replace(":id", book.id))}
                        >
                          Подробнее
                        </Button>
                        {user.userInfo.role === "Admin" && (
                          <>
                            <Button
                              variant="danger"
                              onClick={() => handleDeleteBook(book.id)}
                              className="ml-2"
                            >
                              Удалить
                            </Button>
                          </>
                        )}
                      </>
                    ) : (
                      <span className="text-danger">Требуется авторизация</span>
                    )}
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
        </Card>
      ))}
    </Container>
  );
};

export default BookListPage;
