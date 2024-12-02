import React, { useEffect, useState, useContext } from "react";
import { Container, Table, Button, Alert, Spinner } from "react-bootstrap";
import { Context } from "../index";
import { getUserBooks, returnBook } from "../http/bookHasUserApi";

const calculateReturnDate = (timeBorrowed, checkoutPeriod) => {
  if (!timeBorrowed || !checkoutPeriod) return "";
  const borrowedDate = new Date(timeBorrowed);
  borrowedDate.setDate(borrowedDate.getDate() + checkoutPeriod);
  return borrowedDate.toLocaleDateString();
};

const UserBooksPage = () => {
  const { user } = useContext(Context);
  const [userBooks, setUserBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    const fetchUserBooks = async () => {
      try {
        setLoading(true);
        const books = await getUserBooks(user.id);
        setUserBooks(books);
      } catch (error) {
        console.error("Error fetching user books:", error);
        setErrorMessage("Не удалось загрузить список книг. Попробуйте позже.");
      } finally {
        setLoading(false);
      }
    };

    fetchUserBooks();
  }, [user.id]);

  const handleReturnBook = async (bookId) => {
    try {
      setSuccessMessage("");
      setErrorMessage("");
      await returnBook(bookId, user.id);
      setSuccessMessage("Книга успешно возвращена!");
      setUserBooks((prevBooks) => prevBooks.filter((book) => book.bookId !== bookId));
    } catch (error) {
      console.error("Error returning book:", error);
      setErrorMessage("Ошибка при возврате книги. Попробуйте позже.");
    }
  };

  if (loading) {
    return (
      <Container className="text-center mt-5">
        <Spinner animation="border" />
        <div>Загрузка...</div>
      </Container>
    );
  }

  return (
    <Container className="mt-5">
      <h2>Мои книги</h2>
      {successMessage && <Alert variant="success">{successMessage}</Alert>}
      {errorMessage && <Alert variant="danger">{errorMessage}</Alert>}
      {userBooks.length > 0 ? (
        <Table striped bordered hover className="mt-3">
          <thead>
            <tr>
              <th>#</th>
              <th>Название</th>
              <th>Жанр</th>
              <th>Срок сдачи</th>
              <th>Действия</th>
            </tr>
          </thead>
          <tbody>
            {userBooks.map((book, index) => {
              const { bookId, bookCharacteristics, timeBorrowed } = book || {};
              const checkoutPeriod = bookCharacteristics?.checkoutPeriod;
              const title = bookCharacteristics?.title || "Неизвестное название";
              const genre = bookCharacteristics?.genre || "Неизвестный жанр";

              return (
                <tr key={bookId}>
                  <td>{index + 1}</td>
                  <td>{title}</td>
                  <td>{genre}</td>
                  <td>{calculateReturnDate(timeBorrowed, checkoutPeriod)}</td>
                  <td>
                    <Button
                      variant="danger"
                      onClick={() => handleReturnBook(bookId)}
                    >
                      Вернуть книгу
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      ) : (
        <div className="mt-3">У вас нет взятых книг.</div>
      )}
    </Container>
  );
};

export default UserBooksPage;
