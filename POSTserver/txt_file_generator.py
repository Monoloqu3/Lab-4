file_name = "test_file.txt"
desired_size_mb = 300
desired_size_bytes = desired_size_mb * 1024 * 1024 
chunk_size = 1024 * 1024  
text_to_write = "Bla bla bla.\n"

with open(file_name, "w") as f:
    while f.tell() < desired_size_bytes:
        f.write(text_to_write * (chunk_size // len(text_to_write)))

with open(file_name, "r+b") as f:
    f.truncate(desired_size_bytes)
