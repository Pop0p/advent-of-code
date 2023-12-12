require 'set'

FILE_PATH = "./input.txt"

def solve(size)
  rope = Array.new(size) { Array.new(2, 0) }
  head = rope[0]
  visited = Set[] 
  File.open(FILE_PATH, "r") do |f|
    f.each_line do |line|
      direction, count = line.split(" ")
      count.to_i.times do |i|
        case direction 
          when "L"
            head[0] -= 1 
          when "R"
            head[0] += 1
          when "U"
            head[1] -= 1
          when "D"
            head[1] += 1
        end 
        rope.each_with_index do |n, ii| 
          if ii == 0
            next
          end
          prev = rope[ii-1]
          dx = prev[0] - n[0]
          dy = prev[1] - n[1]
          if dx.abs > 1 || dy.abs > 1
            n[0] += dx<=>0
            n[1] += dy<=>0
          end
        end
        visited.add([rope[-1][0], rope[-1][1]])
      end
    end
  end
  puts visited.length()
end
solve(2)
solve(10)
