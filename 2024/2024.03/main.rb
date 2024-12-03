FILE_PATH = "./input.txt"

file_content = File.read(FILE_PATH).gsub("\n", ' ')

def calculate_sum(content, filter_regex = nil)
  filtered_content = filter_regex ? content.gsub(filter_regex, '') : content
  filtered_content.scan(/mul\((\d{1,3}),(\d{1,3})\)/).sum { |a, b| a.to_i * b.to_i }
end

sum_one = calculate_sum(file_content)
sum_two = calculate_sum(file_content, /don't\(\)(.*?)(do\(\)|$)/)

puts sum_one
puts sum_two
