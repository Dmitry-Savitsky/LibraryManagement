import React, { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import { Container, Row, Col, Card, Badge, Button, Alert, Modal } from "react-bootstrap";
import { Context } from "../index";
import { getBookCharacteristicById } from "../http/bookCharacteristicsApi";
import { getAuthorById } from "../http/authorApi";
import { reserveBook } from "../http/bookHasUserApi";
import EditBookForm from "../components/EditBookForm";
const BookDetailsPage = () => {
  const { id } = useParams();
  const { user } = useContext(Context); const [book, setBook] = useState(null);
  const [author, setAuthor] = useState(null);
  const [loading, setLoading] = useState(true);
  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [showEditForm, setShowEditForm] = useState(false);
  useEffect(() => {
    const fetchBookDetails = async () => {
      try {
        const bookData = await getBookCharacteristicById(id);
        setBook(bookData);

        if (bookData.authorId) {
          const authorData = await getAuthorById(bookData.authorId);
          setAuthor(authorData);
        }
      } catch (error) {
        console.error("Error fetching book details:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchBookDetails();
  }, [id]);

  const handleReserveBook = async () => {
    try {
      setSuccessMessage("");
      setErrorMessage("");

      if (!user.isAuthenticated) {
        setErrorMessage("Вы должны быть авторизованы, чтобы взять книгу.");
        return;
      }

      await reserveBook(book.id, user.id); setSuccessMessage("Книга успешно зарезервирована!");
    } catch (error) {
      console.error("Error reserving book:", error);
      setErrorMessage("Ошибка при резервировании книги. Попробуйте позже.");
    }
  };

  const handleEditClick = () => {
    setShowEditForm(true);
  };

  if (loading) {
    return <div className="text-center mt-5">Загрузка...</div>;
  }

  if (!book) {
    return <div className="text-center mt-5">Книга не найдена</div>;
  }

  return (
    <Container className="mt-5">
      <Row>
        <Col md={4}>
          <Card>
            <Card.Img
              variant="top"
              src={process.env.REACT_APP_API_URL + book.imgPath}
              style={{ height: "400px", objectFit: "cover" }}
            />
          </Card>
        </Col>
        <Col md={8}>
          <h2>{book.title}</h2>
          <h5>
            Жанр: <Badge bg="info">{book.genre || "Без жанра"}</Badge>
          </h5>
          <p>
            <strong>Описание:</strong> {book.description}
          </p>
          <p>
            <strong>ISBN:</strong> {book.isbn}
          </p>
          <p>
            <strong>Срок выдачи:</strong> {book.checkoutPeriod} дней
          </p>
          <p>
            <strong>Количество в наличии:</strong>{" "}
            {book.bookCount > 0 ? (
              `${book.bookCount} шт.`
            ) : (
              <span className="text-danger">Нет в наличии</span>
            )}
          </p>
          {author && (
            <>
              <hr />
              <h4>Об авторе</h4>
              <p>
                <strong>Имя:</strong> {author.name} {author.surename}
              </p>
              <p>
                <strong>Дата рождения:</strong>{" "}
                {new Date(author.birthdate).toLocaleDateString()}
              </p>
              <p>
                <strong>Страна:</strong> {author.country}
              </p>
            </>
          )}
          {book.bookCount > 0 && (
            <Button variant="primary" onClick={handleReserveBook} className="mt-3">
              Взять книгу
            </Button>
          )}
          {successMessage && <Alert variant="success" className="mt-3">{successMessage}</Alert>}
          {errorMessage && <Alert variant="danger" className="mt-3">{errorMessage}</Alert>}

          {/* Кнопка "Редактировать" для Admin */}
          {user.userInfo.role === "Admin" && (
            <Button
              variant="warning"
              className="mt-3"
              onClick={handleEditClick}              >
              Редактировать
            </Button>
          )}
        </Col>
      </Row>

      {/* Модальное окно с формой редактирования */}
      {showEditForm && (
        <Modal show={showEditForm} onHide={() => setShowEditForm(false)} size="lg">
          <Modal.Body>
            <EditBookForm
              bookId={book.id}
              currentTitle={book.title}
              currentGenre={book.genre}
              currentDescription={book.description}
              currentIsbn={book.isbn}
              currentCheckoutPeriod={book.checkoutPeriod}
              currentBookCount={book.bookCount}
              onClose={() => setShowEditForm(false)} />
          </Modal.Body>
        </Modal>
      )}
    </Container>
  );
};

export default BookDetailsPage;
