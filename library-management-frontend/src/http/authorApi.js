import { $host } from "./index";


export const getAllAuthors = async () => {
    try {
        const { data } = await $host.get("api/author");
        return data;
    } catch (error) {
        console.error("Error fetching all authors:", error);
        throw error;
    }
};


export const getAuthorById = async (id) => {
    try {
        const { data } = await $host.get(`api/author/${id}`);
        return data;
    } catch (error) {
        console.error(`Error fetching author by ID ${id}:`, error);
        throw error;
    }
};



export const getAuthorsByCountry = async (country) => {
    try {
        const { data } = await $host.get(`api/author/country`, {
            params: { country },
        });
        return data;
    } catch (error) {
        console.error(`Error fetching authors by country ${country}:`, error);
        throw error;
    }
};


export const addAuthor = async (authorData) => {
    try {
        const { data } = await $host.post("api/author", authorData);
        return data;
    } catch (error) {
        console.error("Error adding new author:", error);
        throw error;
    }
};

export const deleteAuthor = async (id) => {
    try {
      await $host.delete(`api/author/${id}`);
      return { message: "Author deleted successfully." };
    } catch (error) {
      console.error(`Error deleting Author with ID ${id}:`, error);
      throw error;
    }
  };
